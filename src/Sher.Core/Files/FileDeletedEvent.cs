using System;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class FileDeletedEvent : BaseDomainEvent
    {
        public Guid FileId { get; }
        public string FileName { get; }

        public FileDeletedEvent(Guid fileId, string fileName)
        {
            FileId = fileId;
            FileName = fileName;
        }
    }
}