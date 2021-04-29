using System;
using Sher.Core.Files;
using Sher.Core.Files.Uploaders;

namespace Sher.UnitTests.Builders
{
    public class UploaderBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _userId = Guid.NewGuid();

        public UploaderBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public Uploader Build()
        {
            return new(_id, _userId);
        }
    }
}