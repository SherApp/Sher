using System;
using Sher.Core.Base;

namespace Sher.Core.Files.Directories
{
    public class DirectoryDeletedEvent : BaseDomainEvent
    {
        public Guid DirectoryId { get; }

        public DirectoryDeletedEvent(Guid directoryId)
        {
            DirectoryId = directoryId;
        }
    }
}