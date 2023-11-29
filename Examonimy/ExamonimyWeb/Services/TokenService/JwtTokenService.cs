using ExamonimyWeb.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamonimyWeb.Services.TokenService
{
    public class JwtTokenService : ITokenService
    {
     
        private readonly IConfiguration _jwtConfigurations;      

        public JwtTokenService(IConfiguration configuration)
        {        
            _jwtConfigurations = configuration.GetSection("JwtConfigurations");
        }

        public IEnumerable<Claim> CreateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role!.Name)
            };

            return claims;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public SigningCredentials CreateSigningCredentials()
        {
            var key = _jwtConfigurations["Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(User user)
        {
            var jwtSecurityToken = CreateJwtSecurityToken(user);
            var jwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwt;
        }

        private JwtSecurityToken CreateJwtSecurityToken(User user)
        {

            return new JwtSecurityToken(
                issuer: _jwtConfigurations["Issuer"],
                claims: CreateClaims(user),
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtConfigurations["Lifetime"])),
                signingCredentials: CreateSigningCredentials()
                );
        }

        public async Task<ClaimsIdentity> GetClaimsIdentityFromTokenAsync(string token, bool validateLifetime)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations["Key"]!)),
                ValidateLifetime = validateLifetime
            };

            var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenValidationParameters);
            if (!tokenValidationResult.IsValid)
                throw tokenValidationResult.Exception;
            return tokenValidationResult.ClaimsIdentity;
        }
    }
}
