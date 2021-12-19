using JWTAuthAPI.Models;
using JWTAuthAPI.Repositories;

namespace JWTAuthAPI.Services
{
    public class UserService : IUserService
    {
        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(res =>
            
            res.Username.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase) 
            && res.Password.Equals( userLogin.Password));

            return user;


        }
    }
}
