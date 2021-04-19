using System;
using Sher.Core.Base;

namespace Sher.Core.Files.Directories
{
    public class DirectoryCreatedEvent : BaseDomainEvent
    {
        public Guid DirectoryId { get; }
        public Guid? ParentDirectoryId { get; }
        public Guid UploaderId { get; }
        public string Name { get; }

        public DirectoryCreatedEvent(Guid directoryId, Guid? parentDirectoryId, Guid uploaderId, string name)
        {
            DirectoryId = directoryId;
            ParentDirectoryId = parentDirectoryId;
            UploaderId = uploaderId;
            Name = name;
        }
    }
}