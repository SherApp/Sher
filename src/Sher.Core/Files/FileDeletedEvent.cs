using System;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class FileDeletedEvent : BaseDomainEvent
    {
        public Guid FileId { get; }
        public string FileSlug { get; }

        public FileDeletedEvent(Guid fileId, string fileSlug)
        {
            FileId = fileId;
            FileSlug = fileSlug;
        }
    }
}