using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public interface IFileQueue : IFileProcessingQueue
    {
        public Task<FileProcessingItem> DequeueFileAsync(CancellationToken cancellationToken);
    }

    public class FileProcessingQueue : IFileQueue
    {
        private readonly ConcurrentQueue<FileProcessingItem> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void QueueFile(Stream stream, string fileName, Func<IServiceScope, Task> onProcessed = default)
        {
            var copiedStream = new MemoryStream();
            stream.CopyTo(copiedStream);

            _queue.Enqueue(new FileProcessingItem(copiedStream, fileName, onProcessed));
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