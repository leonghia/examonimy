using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Repositories.GenericRepository;
using System.Security.Cryptography;
using System.Text;

namespace ExamonimyWeb.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private const int _keySize = 64;
        private const int _iterations = 3500;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public AuthService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        private bool CheckPassword(User user, string password)
        {
            var passwordHashToCompare = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Convert.FromHexString(user.PasswordSalt!), _iterations, _hashAlgorithm, _keySize);

            return CryptographicOperations.FixedTimeEquals(passwordHashToCompare, Convert.FromHexString(user.PasswordHash!));
        }     

        public Task<string> CreateTokenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetAsync(u => u.Email.Equals(userLoginDto.Email), null);

            return user is not null && CheckPassword(user, userLoginDto.Password);
        }
        
        public string HashPassword(string password, out string passwordSalt)
        {
            var salt = RandomNumberGenerator.GetBytes(_keySize);

            passwordSalt = Convert.ToHexString(salt);

            var passwordHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, _iterations, _hashAlgorithm, _keySize);

            return Convert.ToHexString(passwordHash);

        }
    }
}
