using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;

namespace ExamonimyWeb.Services.AuthService
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(UserLoginDto userLoginDto);
        Task<string> CreateTokenAsync();
        string HashPassword(string password, out string passwordSalt);
    }
}
