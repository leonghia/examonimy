using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories;
using ExamonimyWeb.Utilities;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace ExamonimyWeb.Managers.UserManager
{
    public class UserManager : IUserManager
    {       
        private readonly IGenericRepository<User> _userRepository;
        private const int _keySize = 64;
        private const int _iterations = 3500;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public UserManager(IGenericRepository<User> userRepository)
        {          
            _userRepository = userRepository;
        }

        public bool CheckPassword(User user, string password)
        {
            var passwordHashToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Convert.FromHexString(user.PasswordSalt!), _iterations, _hashAlgorithm, _keySize);

            return CryptographicOperations.FixedTimeEquals(passwordHashToCompare, Convert.FromHexString(user.PasswordHash!));
        }

        public async Task<OperationResult> CreateAsync(User user, string password)
        {
            var operationResult = new OperationResult();

            if (await _userRepository.GetAsync(u => u.NormalizedUsername!.Equals(user.NormalizedUsername), null) is not null)
            {
                operationResult.Succeeded = false;
                operationResult.Errors ??= new List<OperationError>();
                operationResult.Errors.Add(new OperationError { Code = "username", Description = "Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác." });
                return operationResult;
            }
            if (await _userRepository.GetAsync(u => u.NormalizedEmail!.Equals(user.NormalizedEmail), null) is not null)
            {
                operationResult.Succeeded = false;
                operationResult.Errors ??= new List<OperationError>();
                operationResult.Errors.Add(new OperationError { Code = "email", Description = "Email đã tồn tại. Vui lòng chọn email khác." });
                return operationResult;
            }


            // Fill in necessary properties
            user.PasswordHash = HashPassword(password, out string passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.RoleId = 2;

            await _userRepository.InsertAsync(user);
            await _userRepository.SaveAsync();
            return operationResult;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _userRepository.GetAsync(u => u.NormalizedEmail!.Equals(email.ToUpperInvariant()), new List<string> { "Role" });
        }

        public async Task<User?> FindByUsernameAsync(string username)
        {
            return await _userRepository.GetAsync(u => u.NormalizedUsername!.Equals(username.ToUpperInvariant()), new List<string> { "Role" });
        }

        public string HashPassword(string password, out string passwordSalt)
        {
            var salt = RandomNumberGenerator.GetBytes(_keySize);

            passwordSalt = Convert.ToHexString(salt);

            var passwordHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, _iterations, _hashAlgorithm, _keySize);

            return Convert.ToHexString(passwordHash);
        }

        public async Task UpdateAsync(User user)
        {
            _userRepository.Update(user);
            await _userRepository.SaveAsync();
        }

        public string GetRole(User user)
        {
            return user.Role!.Name;
        }

        public async Task<User?> GetByIdAsync(object id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetRangeAsync(Expression<Func<User, bool>>? predicate = null, List<string>? includedProps = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null)
        {
            return await _userRepository.GetRangeAsync(predicate, includedProps, orderBy);
        }
    }
}
