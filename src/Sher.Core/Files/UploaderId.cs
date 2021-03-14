using System;

namespace Sher.Core.Files
{
    public abstract class UploaderId : IComparable<UploaderId>
    {
        public Guid Value { get; }

        protected UploaderId(Guid value)
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