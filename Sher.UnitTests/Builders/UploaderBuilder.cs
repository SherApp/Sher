using System;
using Sher.Core.Files;
using Sher.Core.Files.Uploaders;

namespace Sher.UnitTests.Builders
{
    public class UploaderBuilder
    {
        private Guid _guid = Guid.NewGuid();

        public UploaderBuilder WithId(Guid uploaderId)
        {
            _guid = uploaderId;
            return this;
        }

        public Uploader Build()
        {
            return new(_guid);
        }
    }
}