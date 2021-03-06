using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sher.Infrastructure.FileProcessing;
using Sher.SharedKernel.Options;
using Xunit;

namespace Sher.UnitTests.Infrastructure.FileProcessing
{
    public class FilePersistenceServiceTests
    {
        [Fact]
        public async Task PersistFileAsync_ValidFileName_CreatesDirectoryAndSavesFile()
        {
            // Arrange
            const string baseDirectory = "/home/test";
            const string directoryName = "dir";
            const string fileName = "file.jpg";

            var fileContents = new byte[] {1, 2, 3};
            var fullFileName = Path.Combine(directoryName, fileName);
            var absolutePath = Path.Combine(baseDirectory, fullFileName);

            var options = new OptionsWrapper<FilePersistenceServiceOptions>(new FilePersistenceServiceOptions
                {UploadsDirectory = baseDirectory});

            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {baseDirectory, new MockDirectoryData()}
            });
            
            await using var fileStream = new MemoryStream(fileContents);

            var service = new FilePersistenceService(options, mockFileSystem);
            
            // Act
            await service.PersistFileAsync(fileStream, fullFileName);

            // Assert
            var file = mockFileSystem.GetFile(absolutePath);

            Assert.True(mockFileSystem.FileExists(absolutePath));
            Assert.Equal(fileContents, file.Contents);
        }
    }
}