using System;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class File : BaseEntity
    {
        public Guid Id { get; private set; }
        public Guid UploaderId { get; private set; }
        public Guid DirectoryId { get; private set; }
        public string FileName { get; private set; }
        public long Length { get; private set; }
        public FileStatus Status { get; private set; } = FileStatus.Uploaded;
        public bool IsDeleted { get; private set; }

        internal File(Guid id, Guid directoryId, Guid uploaderId, string fileName, long length)
        {
            Id = id;
            DirectoryId = directoryId;
            UploaderId = uploaderId;
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

        public void Delete()
        {
            IsDeleted = true;
            AddDomainEvent(new FileDeletedEvent(this.Id, FileName));
        }
    }

    public enum FileStatus
    {
        Uploaded,
        Processed
    }
}