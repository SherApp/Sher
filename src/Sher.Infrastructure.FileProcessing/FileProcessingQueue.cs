using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public interface IFileQueue<TContext> : IFileProcessingQueue<TContext>
    {
        public Task<FileProcessingItem<TContext>> DequeueFileAsync(CancellationToken cancellationToken);
    }

    public class FileProcessingQueue<TContext> : IFileQueue<TContext>
    {
        private readonly ConcurrentQueue<FileProcessingItem<TContext>> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void QueueFile(Stream stream, string fileName, TContext context)
        {
            var copiedStream = new MemoryStream();
            stream.CopyTo(copiedStream);

            _queue.Enqueue(new FileProcessingItem<TContext>(copiedStream, fileName, context));
            _signal.Release();
        }

        public async Task<FileProcessingItem<TContext>> DequeueFileAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);

            _queue.TryDequeue(out var item);
            return item;
        }
    }
}