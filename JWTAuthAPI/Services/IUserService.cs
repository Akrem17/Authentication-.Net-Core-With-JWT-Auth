using JWTAuthAPI.Models;

namespace JWTAuthAPI.Services
{
    public interface IUserService
    {
        public User Get(UserLogin userLogin);
    }
}
