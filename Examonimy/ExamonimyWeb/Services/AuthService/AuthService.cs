using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Models.DTOs.UserDTO;

namespace ExamonimyWeb.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;                   

        public AuthService(IUserManager userManager)
        {
            _userManager = userManager;       
            
        }                             

        public async Task<User?> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user is not null && _userManager.CheckPassword(user, userLoginDto.Password))
                return user;
            return null;
        }
              
    }
}
