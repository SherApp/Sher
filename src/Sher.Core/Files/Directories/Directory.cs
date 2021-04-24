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

        public Directory(Guid id, Guid? parentDirectoryId, Guid uploaderId, string name)
        {
            Id = id;
            ParentDirectoryId = parentDirectoryId;
            UploaderId = uploaderId;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid directory name");

            Name = name;
        }
        
        public static ISpecification<Directory> IsRootFor(Guid uploaderId)
        {
            return new Spec<Directory>(d =>
                d.ParentDirectoryId == null && d.UploaderId == uploaderId);
        }
    }
}