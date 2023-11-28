using ExamonimyWeb.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ExamonimyWeb.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken();
        SigningCredentials CreateSigningCredentials();
        IEnumerable<Claim> CreateClaims(User user);
        string CreateRefreshToken();
        Task<ClaimsIdentity> GetClaimsIdentityFromTokenAsync(string token, bool validateLifetime);
    }
}
