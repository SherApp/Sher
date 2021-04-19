using System;
using System.IO;
using Sher.Core.Base;
using Sher.Core.Files.Events;

namespace Sher.Core.Files
{
    public class Directory : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid? ParentDirectoryId { get; private set; }
        public Guid UploaderId { get; private set; }
        public string Name { get; private set; }

        public Directory(Guid id, Guid? parentDirectoryId, Guid uploaderId, string name)
        {
            Id = id;
            ParentDirectoryId = parentDirectoryId;
            UploaderId = uploaderId;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid directory name");

            Name = name;
        }
        
        public File UploadFile(Guid id, string fileName, long length, Stream fileStream)
        {
            AddDomainEvent(new FileUploadedEvent(id, this.Id, UploaderId, fileName, fileStream));
            return new File(id, this.Id, UploaderId, fileName, length);
        }
    }
}