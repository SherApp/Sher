using System.Threading.Tasks;
using Sher.IntegrationTests.Fixtures;
using Sher.IntegrationTests.Utils;
using Xunit;

namespace Sher.IntegrationTests.FileManagement
{
    public class UploadFileTests : IClassFixture<RegisteredUserFixture>
    {
        private readonly RegisteredUserFixture _fixture;

        public UploadFileTests(RegisteredUserFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldSaveFile()
        {
            var client = await _fixture.GetOrCreateAuthorizedClient("upload@example.com");

            const string fileContents = "123";

            var location = await client.SendTusFileAsync(fileContents);

            var receivedContents = await client.GetStringAsync(location);
            
            Assert.Equal(fileContents, receivedContents);
        }
    }
}