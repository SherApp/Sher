using System.Threading.Tasks;
using Moq;
using Sher.Core.Access;
using Sher.Core.Access.Users;
using Sher.UnitTests.Builders;
using Xunit;

namespace Sher.UnitTests.Core.Access
{
    public class AuthenticationServiceRefreshTokenTests
    {
        [Fact]
        public async Task RefreshUserTokenAsync_ValidRefreshToken_ReturnsAndSetsNewRefreshToken()
        {
            // Arrange
            const string oldRefreshToken = "old_token";
            const string newRefreshToken = "new_token";

            var user = new UserBuilder().WithRefreshToken(oldRefreshToken).Build();
            var repoMock =
                Mock.Of<IUserRepository>(f => f.GetUserByIdAsync(user.Id) == Task.FromResult(user));
            
            var passwordHashingServiceMock = Mock.Of<IPasswordHashingService>(
                x => x.GetRandomToken(It.IsAny<int>()) == newRefreshToken);

            var authService = new AuthenticationService(passwordHashingServiceMock, repoMock);

            // Act
            var result = await authService.RefreshUserTokenAsync(user.Id, oldRefreshToken);
            
            // Assert
            Assert.Equal(user.Id.ToString(), result.NameIdentifier);
            Assert.Equal(newRefreshToken, result.RefreshToken);
            Assert.Equal(newRefreshToken, user.RefreshToken);
        }

        [Fact]
        public async Task RefreshUserTokenAsync_InvalidRefreshToken_ReturnsNull()
        {
            // Arrange
            var user = new UserBuilder().Build();
            var repoMock =
                Mock.Of<IUserRepository>(f => f.GetUserByIdAsync(user.Id) == Task.FromResult(user));
            
            var authService = new AuthenticationService(Mock.Of<IPasswordHashingService>(), repoMock);

            // Act
            var result = await authService.RefreshUserTokenAsync(user.Id, "invalid_token");
            
            // Assert
            Assert.Null(result);
        }
    }
}