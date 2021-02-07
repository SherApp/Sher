using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sher.Infrastructure.FileProcessing.Interfaces;
using Sher.SharedKernel;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingQueue : IFileQueue
    {
        private readonly ConcurrentQueue<FileProcessingItem> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void QueueFile(Stream stream, string fileName, IFileProcessingContext context)
        {
            var copiedStream = new MemoryStream();
            stream.CopyTo(copiedStream);

            _queue.Enqueue(new FileProcessingItem(copiedStream, fileName, context));
            _signal.Release();
        }

        public async Task<FileProcessingItem> DequeueFileAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);

            _queue.TryDequeue(out var item);
            return item;
        }
    }
}