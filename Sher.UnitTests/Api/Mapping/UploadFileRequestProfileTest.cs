using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Sher.Api.Mapping;
using Sher.Api.Models;
using Sher.Application.Commands;
using Xunit;

namespace Sher.UnitTests.Api.Mapping
{
    public class UploadFileRequestProfileTest
    {
        [Fact]
        public void UploadFileRequestProfile_ValidUploadFileRequestModel_MapsToFileUploadCommand()
        {
            // Arrange
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<UploadFileRequestProfile>());
            configuration.AssertConfigurationIsValid();

            using var stream = new MemoryStream();
            const string fileName = "abc.jpg";
            var mockFormFile = Mock.Of<IFormFile>(src => src.OpenReadStream() == stream && src.FileName == fileName);
            var id = Guid.NewGuid();

            var uploadFileRequestModel = new UploadFileRequestModel
            {
                File = mockFormFile,
                Id = id
            };

            var mapper = configuration.CreateMapper();
            
            // Act
            var cmd = mapper.Map<FileUploadCommand>(uploadFileRequestModel);
            
            // Assert
            Assert.Equal(uploadFileRequestModel.Id, cmd.Id);
            Assert.Equal(stream, cmd.FileStream);
            Assert.Equal(Path.Combine(id.ToString(), fileName), cmd.Slug);
            Assert.Equal(fileName, cmd.OriginalFileName);
        }
    }
}