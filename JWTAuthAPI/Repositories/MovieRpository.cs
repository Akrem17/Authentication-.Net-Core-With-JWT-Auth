using JWTAuthAPI.Models;

namespace JWTAuthAPI.Repositories
{
    public class MovieRpository
    {
        public static List<Movie> Movies = new()
        {
            new()
            {
                Id = 0,
                Title = "Breaking bad",
                Description = "Fan",
                Rating = "5"
            },
            new()
            {
                Id = 1,
                Title = "Charnobyl",
                Description = "good",
                Rating = "4"
            },
            new()
            {
                Id = 2,
                Title = "Peaky Blinders",
                Description = "drama",
                Rating = "5"
            }
        };

    }
}
