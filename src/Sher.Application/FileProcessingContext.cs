using System;
using Sher.SharedKernel;

namespace Sher.Application
{
    public class FileProcessingContext : IFileProcessingContext
    {
        public Guid FileId { get; }

        public FileProcessingContext(Guid fileId)
        {
            FileId = fileId;
        }
    }
}