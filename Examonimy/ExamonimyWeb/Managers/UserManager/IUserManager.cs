using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;

namespace ExamonimyWeb.Managers.UserManager
{
    public interface IUserManager
    {
        Task<OperationResult> CreateAsync(User user, string password);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByUsernameAsync(string username);
        bool CheckPassword(User user, string password);
        string HashPassword(string password, out string passwordSalt);
        Task UpdateAsync(User user);
        string GetRole(User user);
    }
}
