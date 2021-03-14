using System;
using System.IO;
using Sher.Core.Base;
using Sher.Core.Files.Events;

namespace Sher.Core.Files
{
    public class Uploader : BaseEntity
    {
        public UploaderId Id { get; }

        public Uploader(UploaderId id)
        {
            Id = id;
        }

        public File UploadFile(Guid id, string fileName, long length, Stream fileStream)
        {
            AddDomainEvent(new FileUploadedEvent(id, fileName, fileStream));
            return new File(id, this.Id, fileName, length);
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