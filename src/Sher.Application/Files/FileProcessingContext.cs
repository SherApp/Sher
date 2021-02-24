using System;
using Sher.SharedKernel;

namespace Sher.Application.Files
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