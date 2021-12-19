using JWTAuthAPI.Models;

namespace JWTAuthAPI.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new()
            {
                Username = "akrem98",
                EmailAddress = "akremhammami@outlook.com",
                Password = "Friendly05",
                GivenName = "Akrem",
                SurName = "Hammami",
                Role = "Admin"

            },
            new()
            {
                Username = "ranim98",
                EmailAddress = "ranimbouzamoucha@gmail.com",
                Password = "Friendly05",
                GivenName = "Ranim",
                SurName = "Bouzamoucha",
                Role = "Standard"

            }
        };
    }
}
