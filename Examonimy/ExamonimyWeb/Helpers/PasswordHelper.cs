using System.Security.Cryptography;
using System.Text;

namespace ExamonimyWeb.Helpers
{
    public static class PasswordHelper
    {

        private const int _keySize = 64;
        private const int _iterations = 3500;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public static string HashPassword(string password, out string passwordSalt)
        {
            var salt = RandomNumberGenerator.GetBytes(_keySize);

            passwordSalt = Convert.ToHexString(salt);

            var passwordHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, _iterations, _hashAlgorithm, _keySize);

            return Convert.ToHexString(passwordHash);
        }
    }
}
