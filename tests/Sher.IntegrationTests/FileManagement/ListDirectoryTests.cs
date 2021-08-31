using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sher.Api.Controllers.Files;
using Sher.Application.Files.ListDirectory;
using Sher.IntegrationTests.Fixtures;
using Sher.IntegrationTests.Utils;
using Xunit;

namespace Sher.IntegrationTests.FileManagement
{
    public class ListDirectoryTests : IClassFixture<TestBaseFixture>
    {
        private readonly RegisteredUserFixture _registeredUserFixture;

        public ListDirectoryTests(TestBaseFixture fixture)
        {
            _registeredUserFixture = fixture.RegisteredUserFixture;
        }

        [Fact]
        public async Task DirectoryOwner_ListDirectory_ReturnDirectoryStructure()
        {
            var client = await _registeredUserFixture.GetOrCreateAuthorizedClient("list@example.com");

            var firstDirId = Guid.NewGuid();

            await client.PostAsJsonAsync("api/directory", new CreateDirectoryRequestModel
            {
                Id = firstDirId,
                Name = "Child 1"
            });

            var secondDirId = Guid.NewGuid();
            
            await client.PostAsJsonAsync("api/directory", new CreateDirectoryRequestModel
            {
                Id = secondDirId,
                ParentDirectoryId = firstDirId,
                Name = "Child 2"
            });

            const string fileName = "file.txt";

            await client.SendTusFileAsync("1234", fileName, secondDirId.ToString());

            var dir = await client.GetFromJsonAsync<DirectoryDto>($"api/Directory/{secondDirId}");
            
            Assert.NotNull(dir);
            Assert.Contains(dir.Files, file => file.FileName is fileName);
            Assert.Equal(dir.Path[1].Id, firstDirId);
        }
    }
}