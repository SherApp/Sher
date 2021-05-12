using System;
using Sher.Core.Files.Directories;

namespace Sher.UnitTests.Builders
{
    public class DirectoryBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid? _parentDirectoryId = null;
        private Guid _uploaderId = Guid.NewGuid();
        private string _name = "Root";

        public DirectoryBuilder WithUploaderId(Guid uploaderId)
        {
            _uploaderId = uploaderId;
            return this;
        }

        public DirectoryBuilder WithParentDirectoryId(Guid parentDirectoryId)
        {
            _parentDirectoryId = parentDirectoryId;
            return this;
        }

        public Directory Build()
        {
            return new(_id, _parentDirectoryId, _uploaderId, _name);
        }
    }
}