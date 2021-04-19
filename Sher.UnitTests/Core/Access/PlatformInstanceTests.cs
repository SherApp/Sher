using System;
using System.Threading.Tasks;
using Moq;
using Sher.Core.Access;
using Sher.Core.Access.Platform;
using Sher.Core.Base;
using Xunit;

namespace Sher.UnitTests.Core.Access
{
    public class PlatformInstanceTests
    {
        [Fact]
        public async Task RegisterUser_ValidParams_RegistersUserWithEncryptedPassword()
        {
            // Arrange
            var userId = Guid.NewGuid();
            const string emailAddress = "example@example.com";
            const string plainTextPassword = "p@ssword";
            const string invitationCode = "invitation-code";
            var hashedPassword = new HashResult
            {
                Hash = "hashed-p@ssword",
                Salt = "salt"
            };
            var platformInstance = new PlatformInstance(new PlatformSettings(invitationCode));
            var passwordHashingServiceMock = Mock.Of<IPasswordHashingService>(s =>
                s.HashPasswordAsync(plainTextPassword) == Task.FromResult(hashedPassword));

            // Act
            var user = await platformInstance.RegisterUser(userId, emailAddress, plainTextPassword,
                passwordHashingServiceMock,
                invitationCode);

            // Assert
            Assert.Equal(userId, user.Id);
            Assert.Equal(emailAddress, user.EmailAddress);
            Assert.Equal(hashedPassword.Hash, user.Password.Hash);
            Assert.Equal(hashedPassword.Salt, user.Password.Salt);
        }

        [Fact]
        public async Task RegisterUser_InvalidInvitationCode_ThrowsBusinessRuleViolationException()
        {
            // Arrange
            var platformInstance = new PlatformInstance(new PlatformSettings("invitation-code"));

            // Act
            var action = new Func<Task>(() => platformInstance.RegisterUser(Guid.NewGuid(), "", "", null, "invalid-invitation-code"));

            // Assert
            await Assert.ThrowsAsync<BusinessRuleViolationException>(action);
        }
    }
}