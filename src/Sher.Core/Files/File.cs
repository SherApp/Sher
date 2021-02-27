using System;
using System.IO;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class File : BaseEntity
    {
        public string Slug => Path.Join(Id.ToString(), OriginalFileName);
        public string UploaderId { get; private set; }
        public string OriginalFileName { get; private set; }
        public long Length { get; private set; }
        public FileStatus Status { get; private set; } = FileStatus.Uploaded;

        public File(Guid id, string uploaderId, string originalFileName, long length) : base(id)
        {
            UploaderId = uploaderId ?? throw new ArgumentNullException(nameof(uploaderId));
            OriginalFileName = originalFileName ?? throw new ArgumentNullException(nameof(originalFileName));
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
    }

    public enum FileStatus
    {
        Uploaded,
        Processed
    }
}