using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Sher.Api;
using Sher.IntegrationTests.Fixtures;
using Sher.IntegrationTests.Utils;
using Xunit;

namespace Sher.IntegrationTests.FileManagement
{
    public class DeleteFileTests : IClassFixture<RegisteredUserFixture>
    {
        private readonly RegisteredUserFixture _fixture;

        public DeleteFileTests(RegisteredUserFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ShouldDeleteFile()
        {
            var client = await _fixture.GetOrCreateAuthorizedClient("delete@example.com");

            const string fileContents = "123";

            var location = await client.SendTusFileAsync(fileContents);

            await client.DeleteAsync(location);

            var res = await client.GetAsync(location);

            Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
        }
    }
}