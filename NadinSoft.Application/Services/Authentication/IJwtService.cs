using System.Security.Claims;

namespace NadinSoft.Application.Services.Authentication
{
    public interface IJwtService
    {
        string GenerateToken(List<Claim> claims);
        List<Claim> GetClaims(string token);
    }
}
