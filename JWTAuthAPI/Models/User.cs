namespace JWTAuthAPI.Models
{
    public class User
    {
        public string Username { get; set; } 
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string Role { get; set; }
    }
}
