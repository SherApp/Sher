using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using Sher.Application.Files.UploadFile;
using Sher.Core.Files;
using Sher.UnitTests.Builders;
using Xunit;
using File = Sher.Core.Files.File;

namespace Sher.UnitTests.Application.Files
{
    public class UploadFileCommandHandlerTests
    {
        [Fact]
        public async Task UploadFileHandler_ValidCommand_CreatesAndAddsFileToRepository()
        {
            // Arrange
            var uploader = new UploaderBuilder().Build();
            File file = null;

            var uploaderRepoMock =
                Mock.Of<IUploaderRepository>(u => u.GetByIdAsync(uploader.Id) == Task.FromResult(uploader));
            var fileRepoMock = new Mock<IFileRepository>();
            fileRepoMock.Setup(r => r.AddAsync(It.IsAny<File>()))
                .Callback<File>(f => Task.FromResult(file = f));

            await using var ms = new MemoryStream(new byte[1]);
            var command = new UploadFileCommand(Guid.NewGuid(), uploader.Id, "123", ms);

            var handler = new UploadFileCommandHandler(uploaderRepoMock, fileRepoMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            fileRepoMock.Verify(r => r.AddAsync(file), Times.Once);

            Assert.Equal(command.Id, file.Id);
            Assert.Equal(command.UploaderId, file.UploaderId);
            Assert.Equal(command.FileName, file.FileName);
        }
    }
}