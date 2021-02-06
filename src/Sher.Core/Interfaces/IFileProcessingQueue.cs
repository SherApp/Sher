using System.IO;

namespace Sher.Core.Interfaces
{
    public interface IFileProcessingQueue<in TContext>
    {
        void QueueFile(Stream stream, string fileName, TContext context);
    }
}