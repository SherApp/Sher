using Sher.Core.Access.Rules;
using Xunit;

namespace Sher.UnitTests.Core.Access.Rules
{
    public class PasswordSecurityRuleTests
    {
        [Fact]
        public void Check_PasswordShorterThanSixCharacters_ReturnsError()
        {
            // Arrange
            const string password = "12345";

            var rule = new PasswordSecurityRule(password);
            
            // Act
            var result = rule.Check();
            
            // Assert
            Assert.Equal("Password must have at least 6 characters", result);
        }

        [Fact]
        public void Check_ValidPassword_ReturnsNull()
        {
            // Arrange
            const string password = "123456";

            var rule = new PasswordSecurityRule(password);
            
            // Act
            var result = rule.Check();
            
            // Assert
            Assert.Null(result);
        }
    }
}