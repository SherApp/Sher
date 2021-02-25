using System;
using System.IO;
using Sher.Core.Base;

namespace Sher.Core.Files
{
    public class File : BaseEntity
    {
        public string Slug => Path.Join(Id.ToString(), OriginalFileName);
        public string OriginalFileName { get; private set; }
        public FileStatus Status { get; private set; } = FileStatus.Uploaded;

        public File(Guid id, string originalFileName) : base(id)
        {
            OriginalFileName = originalFileName ?? throw new ArgumentNullException(nameof(originalFileName));
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