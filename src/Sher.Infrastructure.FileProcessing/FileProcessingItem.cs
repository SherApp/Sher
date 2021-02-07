using System;
using System.IO;
using Sher.SharedKernel;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingItem
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public IFileProcessingContext Context { get; set; }

        public FileProcessingItem(Stream stream, string fileName, IFileProcessingContext context)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Context = context;
        }
    }
}