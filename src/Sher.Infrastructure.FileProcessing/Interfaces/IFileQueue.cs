using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Files;

namespace Sher.Infrastructure.FileProcessing.Interfaces
{
    public interface IFileQueue : IFileProcessingQueue
    {
        public Task<FileProcessingItem> DequeueFileAsync(CancellationToken cancellationToken);
    }
}