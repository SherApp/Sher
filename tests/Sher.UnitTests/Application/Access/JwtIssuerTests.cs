using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Sher.Application.Access;
using Sher.Application.Access.Jwt;
using Xunit;

namespace Sher.UnitTests.Application.Access
{
    public class JwtIssuerTests
    {
        [Fact]
        public void IssueToken_WithRole_IssuesTokenWithRoleClaim()
        {
            // Arrange
            const string userId = "0000-0001";
            const string role = "Admin";

            var issuer = GetJwtIssuer();
            
            // Act
            var token = issuer.IssueToken(userId, role);

            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Equal(role, readToken.Claims.GetValue("role"));
            Assert.Equal(userId, readToken.Claims.GetValue("nameid"));
        }
        
        [Fact]
        public void IssueToken_WithoutRole_IssuesTokenWithoutRoleClaim()
        {
            // Arrange
            const string userId = "0000-0001";

            var issuer = GetJwtIssuer();
            
            // Act
            var token = issuer.IssueToken(userId);

            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.ReadJwtToken(token);

            // Assert
            Assert.Null(readToken.Claims.GetValue("role"));
            Assert.Equal(userId, readToken.Claims.GetValue("nameid"));
        }

        private static JwtIssuer GetJwtIssuer()
        {
            var options = new JwtOptions
            {
                Audience = "MyAudience",
                Issuer = "Test",
                SecurityKey =
                    "DVSR1TD0D0jLuRe4zg3nPp9hMqulIQe9rk1bmX1jVXd364UBOVOG3HzoMFPdMBYDeQzoHBYe99crtv1t5vfkv3URbvYJRjXqOHbW9jDt4yKHEkFrW28XAbzLq9qd5FQj"
            };
            return new JwtIssuer(new OptionsWrapper<JwtOptions>(options));
        }
    }

    public static class JwtExtensions
    {
        public static string GetValue(this IEnumerable<Claim> claims, string claimType) =>
            claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
    }
}