using System;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class File : BaseEntity
    {
        public string UploaderId { get; private set; }
        public string FileName { get; private set; }
        public long Length { get; private set; }
        public FileStatus Status { get; private set; } = FileStatus.Uploaded;

        public File(Guid id, string uploaderId, string fileName, long length) : base(id)
        {
            UploaderId = uploaderId ?? throw new ArgumentNullException(nameof(uploaderId));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            if (length <= 0)
            {
                throw new ArgumentException("File cannot be empty", nameof(length));
            }
            Length = length;
        }

        // EF Constructor
        private File(Guid id) : base(id)
        {
        }

        public void MarkAsProcessed()
        {
            Status = FileStatus.Processed;
        }

        public void Delete()
        {
            if (IsDeleted)
            {
                throw new BusinessRuleViolationException("The file has already been deleted.");
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