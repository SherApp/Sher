using System;
using Sher.Core.Files;

namespace Sher.UnitTests.Builders
{
    public class FileBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _directoryId = Guid.NewGuid();
        private string _fileName = "image.jpg";
        private string _contentType = "image/jpeg";
        private Guid _uploaderId = Guid.NewGuid();
        private long _length = 1024;

        public FileBuilder()
        {
            
        }

        public FileBuilder WithUploaderId(Guid uploaderId)
        {
            _uploaderId = uploaderId;
            return this;
        }

        public FileBuilder WithDirectoryId(Guid directoryId)
        {
            _directoryId = directoryId;
            return this;
        }

        public File Build()
        {
            return new(_id, _directoryId, _uploaderId, _fileName, _contentType, _length);
        }
    }
}