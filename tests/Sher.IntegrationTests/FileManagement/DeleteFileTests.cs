using System.Net;
using System.Threading.Tasks;
using Sher.IntegrationTests.Fixtures;
using Sher.IntegrationTests.Utils;
using Xunit;

namespace Sher.IntegrationTests.FileManagement
{
    public class DeleteFileTests : IClassFixture<TestBaseFixture>
    {
        private readonly RegisteredUserFixture _registeredUserFixture;

        public DeleteFileTests(TestBaseFixture fixture)
        {
            _registeredUserFixture = fixture.RegisteredUserFixture;
        }

        [Fact]
        public async Task DeleteFile_FileUploader_SuccessfullyDeleteFile()
        {
            var client = await _registeredUserFixture.GetOrCreateAuthorizedClient("delete@example.com");

            const string fileContents = "123";

            var location = await client.SendTusFileAsync(fileContents);

            await client.DeleteAsync(location);

            var res = await client.GetAsync(location);

            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }

        [Fact]
        public async Task DeleteFile_UserOtherThanUploader_Return404()
        {
            var uploaderClient = await _registeredUserFixture.GetOrCreateAuthorizedClient("delete_uploader@example.com");
        
            const string fileContents = "123";
        
            var location = await uploaderClient.SendTusFileAsync(fileContents);
        
            var otherUserClient = await _registeredUserFixture.GetOrCreateAuthorizedClient("delete_otherUser@example.com", reuseAnyExistingClient: false);
        
            var res = await otherUserClient.DeleteAsync(location);
            
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}