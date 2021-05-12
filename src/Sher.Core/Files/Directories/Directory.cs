using System;
using NSpecifications;
using Sher.Core.Base;

namespace Sher.Core.Files.Directories
{
    public class Directory : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid? ParentDirectoryId { get; private set; }
        public Guid UploaderId { get; private set; }
        public string Name { get; private set; }
        public bool IsDeleted { get; private set; }

        public Directory(Guid id, Guid? parentDirectoryId, Guid uploaderId, string name)
        {
            Id = id;
            ParentDirectoryId = parentDirectoryId;
            UploaderId = uploaderId;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid directory name");

            Name = name;
        }

        public Directory CreateChildDirectory(Guid directoryId, string name)
        {
            AddDomainEvent(new DirectoryCreatedEvent(directoryId, this.Id, this.UploaderId, name));
            return new Directory(directoryId, this.Id, this.UploaderId, name);
        }

        public void Delete()
        {
            if (this.Is(RootDirectory))
            {
                throw new BusinessRuleViolationException("Cannot delete root directory");
            }

            IsDeleted = true;
            AddDomainEvent(new DirectoryDeletedEvent(this.Id));
        }

        public static ISpecification<Directory> RootDirectory => new Spec<Directory>(d => d.ParentDirectoryId == null);
        
        public static ISpecification<Directory> RootDirectoryFor(Guid? uploaderId = null)
        {
            return new Spec<Directory>(d =>
                d.ParentDirectoryId == null && d.UploaderId == uploaderId);
        }
    }
}