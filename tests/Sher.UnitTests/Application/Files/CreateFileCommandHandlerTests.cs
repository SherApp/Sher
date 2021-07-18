using System;
using System.Threading.Tasks;
using Moq;
using Sher.Application.Files.CreateFile;
using Sher.Core.Files;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;
using Sher.UnitTests.Builders;
using Xunit;
using File = Sher.Core.Files.File;

namespace Sher.UnitTests.Application.Files
{
    public class CreateFileCommandHandlerTests
    {
        [Fact]
        public async Task CreateFileHandle_ValidCommand_CreatesAndAddsFileToRepository()
        {
            // Arrange
            var uploader = new UploaderBuilder().Build();
            File file = null;

            var directory = new DirectoryBuilder()
                .WithUploaderId(uploader.Id)
                .Build();

            var uploaderRepoMock =
                Mock.Of<IUploaderRepository>(u => u.GetByUserIdAsync(uploader.UserId) == Task.FromResult(uploader));
            var directoryRepoMock =
                Mock.Of<IDirectoryRepository>(d => d.GetWithOrRootAsync(directory.Id, directory.UploaderId) == Task.FromResult(directory));

            var fileRepoMock = new Mock<IFileRepository>();
            fileRepoMock.Setup(r => r.AddAsync(It.IsAny<File>()))
                .Callback<File>(f => Task.FromResult(file = f));
            
            var command = new CreateFileCommand(Guid.NewGuid(), directory.Id, uploader.UserId, "123", "image/png", 10);

            var handler = new CreateFileCommandHandler(directoryRepoMock, uploaderRepoMock, fileRepoMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            fileRepoMock.Verify(r => r.AddAsync(file), Times.Once);

            Assert.Equal(command.Id, file.Id);
            Assert.Equal(uploader.Id, file.UploaderId);
            Assert.Equal(command.FileName, file.FileName);
            Assert.Equal(command.ContentType, file.ContentType);
            Assert.Equal(directory.Id, file.DirectoryId);
            Assert.Equal(command.FileLength, file.Length);
        }
    }
}