using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingItem<TContext>
    {
        public Stream Stream { get; set; }
        public string FileName { get; set; }
        public TContext Context { get; set; }

        public FileProcessingItem(Stream stream, string fileName, TContext context)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Context = context;
        }
    }
}