using System;
using Sher.Core.Base;
using Sher.Core.Files.Events;

namespace Sher.Core.Files
{
    public class File : BaseEntity
    {
        public Guid Id { get; private set; }
        public UploaderId UploaderId { get; private set; }
        public string FileName { get; private set; }
        public long Length { get; private set; }
        public FileStatus Status { get; private set; } = FileStatus.Uploaded;

        internal File(Guid id, UploaderId uploaderId, string fileName, long length)
        {
            Id = id;
            UploaderId = uploaderId ?? throw new ArgumentNullException(nameof(uploaderId));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            if (length <= 0)
            {
                throw new ArgumentException("File cannot be empty", nameof(length));
            }
            Length = length;
        }

        // EF Constructor
        private File()
        {
        }

        public void MarkAsProcessed()
        {
            Status = FileStatus.Processed;
        }

        public void Delete(Guid uploaderId)
        {
            if (IsDeleted)
            {
                throw new BusinessRuleViolationException("The file has already been deleted.");
            }

            if (uploaderId != Id)
            {
                throw new BusinessRuleViolationException("File can only be deleted by its owner.");
            }

            IsDeleted = true;
            AddDomainEvent(new FileDeletedEvent(Id, FileName));
        }
    }

    public enum FileStatus
    {
        Uploaded,
        Processed
    }
}