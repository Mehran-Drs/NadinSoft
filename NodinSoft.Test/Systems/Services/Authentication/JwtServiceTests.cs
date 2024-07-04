using Moq;
using NadinSoft.Application.Services.Authentication.JwtServices;
using System.Security.Claims;

namespace NodinSoft.Test.Systems.Services.Authentication
{
    public class JwtServiceTests
    {
        [Fact]
        public async Task GetClaimsTest()
        {
            // Arrange
            var token = "Bearer";

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

            var mockJwt = new Mock<IJwtService>();

            mockJwt.Setup(j => j.GetClaims(It.IsAny<string>())).Returns(claims);

            // Act
            var getClaimsMethod = mockJwt.Object.GetClaims(token);

            // Assert
            Assert.Equal(claims.Count, getClaimsMethod.Count()); 
            Assert.All(claims, expectedClaim => Assert.Contains(getClaimsMethod, actualClaim =>
                actualClaim.Type == expectedClaim.Type && actualClaim.Value == expectedClaim.Value));
        }

        [Fact]
        public async Task GenerateTokenTest()
        {
            //Arrange
            var token = "Bearer";
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "John Doe"),
                    new Claim(ClaimTypes.Role, "Admin"),
                };
            var mockJwt = new Mock<IJwtService>();

            mockJwt.Setup(j => j.GenerateToken(It.IsAny<List<Claim>>())).Returns(token);

            // Act
            var generateTokenMethod = mockJwt.Object.GenerateToken(claims);

            // Assert
            Assert.Equal(token,generateTokenMethod);
        }
    }
}
