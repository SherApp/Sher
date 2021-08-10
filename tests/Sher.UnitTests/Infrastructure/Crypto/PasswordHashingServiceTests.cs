using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Sher.Infrastructure.Crypto;
using Xunit;

namespace Sher.UnitTests.Infrastructure.Crypto
{
    public class PasswordHashingServiceTests
    {
        [Fact]
        public async Task HashPasswordAsync_ValidPassword_HashesPassword()
        {
            var password = "password";
            var options = new PasswordHashingOptions
            {
                Iterations = 1,
                DegreeOfParallelism = 1,
                MemorySize = 128
            };
            var hashingService = new PasswordHashingService(Mock.Of<IOptions<PasswordHashingOptions>>(opt => opt.Value == options));
            var result = await hashingService.HashPasswordAsync(password);

            var isPasswordCorrect = await hashingService.VerifyPasswordAsync(result.Hash, password, result.Salt);
            
            Assert.True(isPasswordCorrect);
        }
    }
}