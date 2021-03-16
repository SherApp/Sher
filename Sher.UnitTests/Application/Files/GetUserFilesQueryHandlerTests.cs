using System;
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
            var uploaderId = Guid.NewGuid();

            var mockRepository = new Mock<IFileRepository>();
            var handler = new GetUploaderFilesQueryHandler(mockRepository.Object, Mock.Of<IMapper>());

            // Act
            handler.Handle(new GetUploaderFilesQuery(uploaderId, requiredFileNamePart), default);

            // Assert
            mockRepository.Verify(
                e => e.SearchAsync(It.Is<FileSearchCriteria>(
                    c => c.RequiredFileNamePart == requiredFileNamePart && c.UploaderId == uploaderId)),
                Times.Once);
        }
    }
}