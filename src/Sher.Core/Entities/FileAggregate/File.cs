using System;

namespace Sher.Core.Entities.FileAggregate
{
    public class File : BaseEntity
    {
        public string Slug { get; private set; }
        public string OriginalFileName { get; private set; }

        public File(Guid id, string slug, string originalFileName) : base(id)
        {
            Slug = slug ?? throw new ArgumentNullException(nameof(slug));
            OriginalFileName = originalFileName ?? throw new ArgumentNullException(nameof(originalFileName));
        }
    }
}