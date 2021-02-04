using System;

namespace Sher.Core.Entities.FileAggregate
{
    public class File : BaseEntity
    {
        public string FileName { get; private set; }
        public string OriginalFileName { get; private set; }

        public File(Guid id, string fileName, string originalFileName) : base(id)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            OriginalFileName = originalFileName ?? throw new ArgumentNullException(nameof(originalFileName));
        }
    }
}