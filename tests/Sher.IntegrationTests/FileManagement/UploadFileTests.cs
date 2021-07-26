using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Sher.IntegrationTests.Fixtures;
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
            var client = await _fixture.GetOrCreateAuthorizedClient();

            const string fileContents = "123";

            var location = await SendFile(client, fileContents);

            var receivedContents = await client.GetStringAsync(location);
            
            Assert.Equal(fileContents, receivedContents);
        }

        [Fact]
        public async Task ShouldDeleteFile()
        {
            var client = await _fixture.GetOrCreateAuthorizedClient();

            const string fileContents = "123";

            var location = await SendFile(client, fileContents);
            
            await client.DeleteAsync(location);

            var res = await client.GetAsync(location);
            
            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }

        private async Task<string> SendFile(HttpClient httpClient, string content)
        {
            var byteContent = Encoding.UTF8.GetBytes(content);
            using var message = new HttpRequestMessage(HttpMethod.Post, "/api/file")
            {
                Content = new ByteArrayContent(byteContent)
            };
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/offset+octet-stream");

            message.Headers.Add("Upload-Metadata", $"fileName {Convert.ToBase64String(Encoding.UTF8.GetBytes("file.txt"))}");
            message.Headers.Add("Tus-Resumable", "1.0.0");
            message.Headers.Add("Upload-Length", byteContent.Length.ToString());

            var res = await httpClient.SendAsync(message);

            return res.Headers.Location!.OriginalString;
        }
    }
}