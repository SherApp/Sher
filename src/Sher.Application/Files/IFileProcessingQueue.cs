using System.IO;
using Sher.SharedKernel;

namespace Sher.Application.Files
{
    public interface IFileProcessingQueue
    {
        void QueueFile(Stream stream, string fileName, IFileProcessingContext context);
    }
}