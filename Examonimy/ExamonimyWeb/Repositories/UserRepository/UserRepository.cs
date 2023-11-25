using ExamonimyWeb.DatabaseContexts;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Models;
using ExamonimyWeb.Repositories.GenericRepository;
using ExamonimyWeb.Services.AuthService;

namespace ExamonimyWeb.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IAuthService _authService;

        public UserRepository(ExamonimyContext context, IAuthService authService) : base(context)
        {
            _authService = authService;
        }

        public async Task<OperationResult> CreateAsync(User user, string password)
        {           
            var operationResult = new OperationResult();
            
            if (await base.GetAsync(u => u.NormalizedUsername!.Equals(user.NormalizedUsername), null) is not null)
            {              
                operationResult.Succeeded = false;
                operationResult.Errors ??= new List<OperationError>();
                operationResult.Errors.Add(new OperationError { Code = "username", Description = "Username already exists. Please choose a different username." });
                return operationResult;
            }
            if (await base.GetAsync(u => u.NormalizedEmail!.Equals(user.NormalizedEmail), null) is not null)
            {               
                operationResult.Succeeded = false;
                operationResult.Errors ??= new List<OperationError>();
                operationResult.Errors.Add(new OperationError { Code = "email", Description = "Email already exists. Please choose a different email." });
                return operationResult;
            }
            

            // Fill in necessary properties
            user.PasswordHash = _authService.HashPassword(password, out string passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.RoleId = 2;

            await base.InsertAsync(user);
            await base.SaveAsync();
            return operationResult;
        }
    }
}
