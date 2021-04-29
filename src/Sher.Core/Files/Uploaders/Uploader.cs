using System;
using System.IO;
using Sher.Core.Base;
using Sher.Core.Files.Directories;
using Directory = Sher.Core.Files.Directories.Directory;

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

        public File UploadFile(Guid fileId, Guid directoryId, string fileName, long length, Stream fileStream)
        {
            AddDomainEvent(new FileUploadedEvent(fileId, directoryId, this.Id, fileName, fileStream));
            return new File(fileId, directoryId, this.Id, fileName, length);
        }
    }
}