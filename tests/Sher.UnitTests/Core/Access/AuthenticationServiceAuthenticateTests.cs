using System.Linq;
using System.Threading.Tasks;
using Moq;
using Sher.Core.Access;
using Sher.Core.Access.Users;
using Sher.UnitTests.Builders;
using Xunit;

namespace Sher.UnitTests.Core.Access
{
    public class AuthenticationServiceAuthenticateTests
    {
        [Fact]
        public async Task AuthenticateUserAsync_ValidCredentials_ReturnsUserDescriptorAndSetsRefreshToken()
        {
            const string plainTextPass = "p@ssword";
            const string refreshToken = "tokeeeeeeen";

            var user = new UserBuilder().Build();
            var repoMock =
                Mock.Of<IUserRepository>(f => f.GetByEmailAddressAsync(user.EmailAddress) == Task.FromResult(user));

            var passwordHashingServiceMock = new Mock<IPasswordHashingService>();
            passwordHashingServiceMock
                .Setup(p => p.VerifyPasswordAsync(user.Password.Hash, plainTextPass, user.Password.Salt))
                .ReturnsAsync(true);
            passwordHashingServiceMock
                .Setup(p => p.GetRandomToken(It.IsAny<int>()))
                .Returns(refreshToken);

            var authService = new AuthenticationService(
                passwordHashingServiceMock.Object,
                repoMock);

            var result = await authService.AuthenticateUserAsync(user.EmailAddress, plainTextPass);

            Assert.Equal(user.Id.ToString(), result.NameIdentifier);
            Assert.Equal(refreshToken, result.RefreshToken);
            Assert.Equal(refreshToken, user.Clients[0].RefreshToken);
        }
        
        [Fact]
        public async Task AuthenticateUserAsync_InvalidCredentials_ReturnsNull()
        {
            var user = new UserBuilder().Build();
            var repoMock =
                Mock.Of<IUserRepository>(f => f.GetByEmailAddressAsync(user.EmailAddress) == Task.FromResult(user));

            var passwordHashingServiceMock = new Mock<IPasswordHashingService>();
            passwordHashingServiceMock
                .Setup(p => p.VerifyPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var authService = new AuthenticationService(
                passwordHashingServiceMock.Object,
                repoMock);

            var result = await authService.AuthenticateUserAsync(user.EmailAddress, "wrong_password");

            Assert.Null(result);
        }
    }
}