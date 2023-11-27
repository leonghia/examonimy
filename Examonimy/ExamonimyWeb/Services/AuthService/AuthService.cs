using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamonimyWeb.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;    
        private readonly IConfiguration _jwtConfigurations;
        
        private User? _user;

        public AuthService(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;       
            _jwtConfigurations = configuration.GetSection("JwtConfigurations");
        }           

        public string CreateJwt()
        {
            var jwtSecurityToken = CreateJwtSecurityToken(_user!);
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

        private SigningCredentials CreateSigningCredentials()
        {
            var key = _jwtConfigurations["Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        private IEnumerable<Claim> CreateClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role!.Name)
            };

            return claims;
        }

        public async Task<User?> ValidateUserAsync(UserLoginDto userLoginDto)
        {
            _user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (_user is not null && _userManager.CheckPassword(_user, userLoginDto.Password))
                return _user;
            return null;
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

        public async Task<ClaimsIdentity> GetClaimsIdentityFromTokenAsync(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations["Key"]!)),
                ValidateLifetime = false
            };

            var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenValidationParameters);
            if (!tokenValidationResult.IsValid)
                throw tokenValidationResult.Exception;
            return tokenValidationResult.ClaimsIdentity;
            
        }
              
    }
}
