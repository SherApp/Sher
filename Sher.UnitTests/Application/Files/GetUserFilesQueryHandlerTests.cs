using AutoMapper;
using Moq;
using Sher.Application.Files.GetUploaderFiles;
using Sher.Core.Base;
using Sher.Core.Files;
using Xunit;

namespace Sher.UnitTests.Application.Files
{
    public class GetUserFilesQueryHandlerTests
    {
        [Fact]
        public void GetUserFilesHandle_ValidQuery_QueriesRepositoryWithCriteria()
        {
            // Arrange
            const string requiredFileNamePart = "image";
            const string uploaderId = "clients@client";

            var mockRepository = new Mock<IRepository<File>>();
            var handler = new GetUploaderFilesQueryHandler(mockRepository.Object, Mock.Of<IMapper>());

            // Act
            handler.Handle(new GetUploaderFilesQuery(uploaderId, requiredFileNamePart), default);

            // Assert
            mockRepository.Verify(
                e => e.ListAsync(It.Is<FileSearchCriteria>(
                    c => c.RequiredFileNamePart == requiredFileNamePart && c.UploaderId == uploaderId)),
                Times.Once);
        }
    }
}