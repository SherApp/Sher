using System;

namespace Sher.SharedKernel
{
    public interface IFileProcessingContext
    {
        public Guid FileId { get; }
    }
}