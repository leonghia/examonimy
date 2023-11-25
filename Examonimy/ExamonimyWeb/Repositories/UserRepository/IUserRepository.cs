using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;

namespace ExamonimyWeb.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<OperationResult> CreateAsync(User user, string password);
    }
}
