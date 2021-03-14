using System;
using System.Collections.Generic;
using System.IO;
using Sher.Core.Base;
using Sher.Core.Files.Events;

namespace Sher.Core.Files
{
    public class Uploader : BaseEntity
    {
        public UploaderId Id { get; }

        public Uploader(UploaderId id)
        {
            Id = id;
        }

        public File UploadFile(Guid id, string fileName, long length, Stream fileStream)
        {
            AddDomainEvent(new FileUploadedEvent(id, fileName, fileStream));
            return new File(id, this.Id, fileName, length);
        }
    }
}