using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Sher.Application.Files;
using Sher.Application.Files.UploadFile;
using Sher.Core.Base;
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
            File file = null;

            var repoMock = new Mock<IFileRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<File>()))
                .Callback<File>(f => Task.FromResult(file = f));

            await using var ms = new MemoryStream(new byte[1]);
            var command = new UploadFileCommand(Guid.NewGuid(), "123", "123", ms);

            IRequestHandler<UploadFileCommand> handler =
                new UploadFileCommandHandler(Mock.Of<IFileProcessingQueue>(), repoMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            repoMock.Verify(r => r.AddAsync(file), Times.Once);

            Assert.Equal(command.Id, file.Id);
            Assert.Equal(command.UploaderId, file.UploaderId);
            Assert.Equal(command.FileName, file.FileName);
        }
    }
}