using ExamonimyWeb.Entities;
using ExamonimyWeb.Models.DTOs.UserDTO;
using System.Security.Claims;

namespace ExamonimyWeb.Services.AuthService
{
    public interface IAuthService
    {
        Task<User?> ValidateUserAsync(UserLoginDto userLoginDto);     
    }
}
