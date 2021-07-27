using System.Threading.Tasks;
using Sher.IntegrationTests.Fixtures;
using Sher.IntegrationTests.Utils;
using Xunit;

namespace Sher.IntegrationTests.FileManagement
{
    public class UploadFileTests : IClassFixture<TestBaseFixture>
    {
        private readonly RegisteredUserFixture _registeredUserFixture;

        public UploadFileTests(TestBaseFixture fixture)
        {
            _registeredUserFixture = fixture.RegisteredUserFixture;
        }

        [Fact]
        public async Task UploadFile_ValidRequest_UploadAndServeFile()
        {
            var client = await _registeredUserFixture.GetOrCreateAuthorizedClient("upload@example.com");

            const string fileContents = "123";

            var location = await client.SendTusFileAsync(fileContents);

            var receivedContents = await client.GetStringAsync(location);
            
            Assert.Equal(fileContents, receivedContents);
        }
    }
}