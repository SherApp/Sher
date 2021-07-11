using System;
using System.IO;
using Sher.Core.Base;

namespace Sher.Core.Files.Uploaders
{
    public class Uploader : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public Uploader(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }

        public File UploadFile(Guid fileId, Guid directoryId, string fileName, long length)
        {
            AddDomainEvent(new FileUploadedEvent(fileId, directoryId, this.Id, fileName));
            return new File(fileId, directoryId, this.Id, fileName, length);
        }
    }
}