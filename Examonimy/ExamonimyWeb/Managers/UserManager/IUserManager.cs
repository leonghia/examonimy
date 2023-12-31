using ExamonimyWeb.Entities;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;

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
        Task<User?> GetByIdAsync(object id);
        Task<IEnumerable<User>> GetRangeAsync(Expression<Func<User, bool>>? predicate = null, List<string>? includedProps = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null);
    }
}
