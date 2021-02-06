using System;
using System.IO;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingItem : IFileProcessingContext
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public Action<IFileProcessingContext> OnProcessed { get; set; }

        public FileProcessingItem(Stream stream, string fileName, Action<IFileProcessingContext> onProcessed)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            OnProcessed = onProcessed;
        }
    }
}