using System;
using System.Threading.Tasks;
using Moq;
using Sher.Application.Files.DeleteFile;
using Sher.Core.Files;
using Sher.UnitTests.Builders;
using Xunit;

namespace Sher.UnitTests.Application.Files
{
    public class DeleteFileCommandHandlerTests
    {
        [Fact]
        public async Task DeleteFileHandle_FileUploadedByUser_DeletesFile()
        {
            // Arrange
            var uploader = new UploaderBuilder().Build();
            var file = new FileBuilder().WithUploaderId(uploader.Id).Build();

            var uploaderRepoMock =
                Mock.Of<IUploaderRepository>(u => u.GetByIdAsync(file.UploaderId) == Task.FromResult(uploader));
            var fileRepoMock = Mock.Of<IFileRepository>(f => f.GetByIdAsync(file.Id) == Task.FromResult(file));

            var handler = new DeleteFileCommandHandler(fileRepoMock, uploaderRepoMock);
            
            // Act
            var result = await handler.Handle(new DeleteFileCommand(file.Id, file.UploaderId), default);
            
            // Assert
            Assert.True(file.IsDeleted);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteFileHandle_FileNotUploadedByUser_ReturnsFalse()
        {
            // Arrange
            var file = new FileBuilder().Build();
            var repoMock = Mock.Of<IFileRepository>(f => f.GetByIdAsync(file.Id) == Task.FromResult(file));

            var handler = new DeleteFileCommandHandler(repoMock, Mock.Of<IUploaderRepository>());
            
            // Act
            var result = await handler.Handle(new DeleteFileCommand(file.Id, Guid.Empty), default);

            // Assert
            Assert.False(result);
        }
    }
}