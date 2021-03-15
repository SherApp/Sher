using System;

namespace Sher.Core.Files
{
    public class UploaderId : IComparable<UploaderId>
    {
        public Guid Value { get; }

        public UploaderId(Guid value)
        {
            Value = value;
        }

        public int CompareTo(UploaderId other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Value.CompareTo(other.Value);
        }
    }
}