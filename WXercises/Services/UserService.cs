using Microsoft.Extensions.Configuration;
using WXercises.Models;

namespace WXercises.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUser()
        {
            return new User(_configuration["user:name"], _configuration["user:token"]);
        }
    }
}
