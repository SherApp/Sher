using System;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class FileDeletedEvent : BaseDomainEvent
    {
        public Guid UploaderId { get; }
        public Guid FileId { get; }
        public string FileName { get; }

        public FileDeletedEvent(Guid uploaderId, Guid fileId, string fileName)
        {
            UploaderId = uploaderId;
            FileId = fileId;
            FileName = fileName;
        }
    }
}