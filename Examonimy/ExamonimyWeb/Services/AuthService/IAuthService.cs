using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using System.Security.Claims;

namespace ExamonimyWeb.Services.AuthService
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(UserLoginDto userLoginDto);     
    }
}
