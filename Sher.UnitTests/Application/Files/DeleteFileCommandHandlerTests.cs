using System;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Sher.Application.Files.DeleteFile;
using Sher.Core.Base;
using Sher.Core.Files;
using Xunit;

namespace Sher.UnitTests.Application.Files
{
    public class DeleteFileCommandHandlerTests
    {
        [Fact]
        public async Task DeleteFileHandle_FileUploadedByUser_DeletesFile()
        {
            // Arrange
            var file = new File(Guid.NewGuid(), "123", "123", 1);
            var repoMock = Mock.Of<IFileRepository>(f => f.GetByIdAsync(file.Id) == Task.FromResult(file));

            IRequestHandler<DeleteFileCommand> handler = new DeleteFileCommandHandler(repoMock);
            
            // Act
            await handler.Handle(new DeleteFileCommand(file.Id, file.UploaderId), default);
            
            // Assert
            Assert.True(file.IsDeleted);
        }

        [Fact]
        public async Task DeleteFileHandle_FileNotUploadedByUser_ThrowsException()
        {
            // Arrange
            var file = new File(Guid.NewGuid(), "123", "123", 1);
            var repoMock = Mock.Of<IFileRepository>(f => f.GetByIdAsync(file.Id) == Task.FromResult(file));

            IRequestHandler<DeleteFileCommand> handler = new DeleteFileCommandHandler(repoMock);
            
            // Act
            var func = new Func<Task>(async () => await handler.Handle(new DeleteFileCommand(file.Id, "other"), default));

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(func);
        }
    }
}