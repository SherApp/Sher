using System;
using Sher.Core.Base;
using Sher.Core.Files.Directories;

namespace Sher.Core.Files.Uploaders
{
    public class Uploader : BaseEntity
    {
        public Guid Id { get; }

        public Uploader(Guid id)
        {
            Id = id;
        }

        public Directory CreateDirectory(Guid directoryId, Guid? parentDirectoryId, string name)
        {
            AddDomainEvent(new DirectoryCreatedEvent(directoryId, parentDirectoryId, this.Id, name));
            return new Directory(directoryId, parentDirectoryId, this.Id, name);
        }

        public void DeleteFile(File file)
        {
            if (file.IsDeleted)
            {
                throw new BusinessRuleViolationException("The file has already been deleted");
            }

            if (file.UploaderId != this.Id)
            {
                throw new BusinessRuleViolationException("The file doesn't belong to this uploader");
            }
            
            file.Delete();
            AddDomainEvent(new FileDeletedEvent(file.Id, file.FileName));
        }
    }
}