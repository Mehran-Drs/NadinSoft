using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NadinSoft.Common.DTOs;
using NadinSoft.Domain.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NadinSoft.Application.Services.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfigDto _jwtConfig;

        public JwtService(IOptions<AthenticationConfigDto> options)
        {
            _jwtConfig = options.Value.Jwt ;
        }

        public string GenerateToken(List<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfig.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpireMinute),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public List<Claim> GetClaims(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token.Split("Bearer ")[1]);

            return jwt.Claims.ToList();
        }
    }
}
