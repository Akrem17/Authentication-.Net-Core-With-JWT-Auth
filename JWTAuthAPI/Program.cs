using JWTAuthAPI.Models;
using JWTAuthAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovie, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello world");
app.MapPost("login",(UserLogin user,IUserService service)=>Login(user, service));
app.MapPost("/create", (Movie movie, IMovie service) => Create(movie, service));
app.MapGet("/get", (int id, IMovie service) => Get(id, service));
app.MapGet("/list",
[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)](IMovie service) => List(service));
app.MapPut("/update", (Movie movie,IMovie service) => Update(movie,service));
app.MapDelete("/delete", (int id,IMovie service) => Delete(id,service));

IResult Login(UserLogin user, IUserService service) {
    if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password)) {
        var loggedUser = service.Get(user);
        if (loggedUser is null) return Results.NotFound("User not found");
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,loggedUser.Username),
            new Claim(ClaimTypes.Email,loggedUser.EmailAddress),
            new Claim(ClaimTypes.GivenName,loggedUser.GivenName),
            new Claim(ClaimTypes.Surname,loggedUser.SurName),
            new Claim(ClaimTypes.Role,loggedUser.Role),
        };

        var token = new JwtSecurityToken(

            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(60),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            SecurityAlgorithms.HmacSha256)  


            ) ;

       var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);

    }
    return Results.NotFound("User not found");
}
IResult Create(Movie movie, IMovie service) {
    var result = service.Create(movie);
    return Results.Ok(result);
        }

IResult Get(int id, IMovie service) {
    var movie = service.Get(id);

    if(movie == null)
    {
        return Results.NotFound("notFound");
    }
    return Results.Ok(movie);
        }

IResult Update(Movie movie, IMovie service) {
    var old = service.Update(movie);

    if(old == null)
    {
        return Results.NotFound("notFound");
    }
    return Results.Ok(old);
        }

IResult Delete(int id, IMovie service) {
    var movie = service.Delete(id);

    if(!movie)
    {
        return Results.NotFound("notFound");
    }
    return Results.Ok(movie);
        }

IResult List( IMovie service)
{
    var movies = service.List();

    return Results.Ok(movies);
}


app.Run();

