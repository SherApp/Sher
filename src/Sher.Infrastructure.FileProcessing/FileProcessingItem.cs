using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingItem : IFileProcessingContext
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public Func<IServiceScope, Task> OnProcessed { get; set; }

        public FileProcessingItem(Stream stream, string fileName, Func<IServiceScope, Task> onProcessed)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            OnProcessed = onProcessed;
        }
    }
}