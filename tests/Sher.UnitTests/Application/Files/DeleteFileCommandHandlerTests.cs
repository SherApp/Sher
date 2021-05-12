using System.Threading.Tasks;
using Moq;
using Sher.Application.Files.DeleteFile;
using Sher.Core.Files;
using Sher.Core.Files.Uploaders;
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
                Mock.Of<IUploaderRepository>(u => u.GetByUserIdAsync(uploader.UserId) == Task.FromResult(uploader));
            var fileRepoMock = Mock.Of<IFileRepository>(f =>
                f.GetWithUploaderIdAsync(uploader.Id, file.Id) == Task.FromResult(file));

            var handler = new DeleteFileCommandHandler(fileRepoMock, uploaderRepoMock);

            // Act
            var result = await handler.Handle(new DeleteFileCommand(file.Id, uploader.UserId), default);

            // Assert
            Assert.True(file.IsDeleted);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteFileHandle_FileNotUploadedByUser_ReturnsFalse()
        {
            // Arrange
            var file = new FileBuilder().Build();
            var uploader = new UploaderBuilder().Build();

            var fileRepoMock = Mock.Of<IFileRepository>(f => f.GetByIdAsync(file.Id) == Task.FromResult(file));
            var uploaderRepoMock =
                Mock.Of<IUploaderRepository>(u => u.GetByUserIdAsync(uploader.UserId) == Task.FromResult(uploader));

            var handler = new DeleteFileCommandHandler(fileRepoMock, uploaderRepoMock);

            // Act
            var result = await handler.Handle(new DeleteFileCommand(file.Id, uploader.UserId), default);

            // Assert
            Assert.False(result);
        }
    }
}