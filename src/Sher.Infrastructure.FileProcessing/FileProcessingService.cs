using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Sher.Core.Interfaces;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingService : BackgroundService
    {
        private readonly IFileQueue _queue;
        private readonly IFilePersistenceService _filePersistenceService;
        public FileProcessingService(IFileQueue queue, IFilePersistenceService filePersistenceService)
        {
            _queue = queue;
            _filePersistenceService = filePersistenceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var file = await _queue.DequeueFileAsync(stoppingToken);

                // TODO: Create a file processing pipeline (eg. compress images)

                await _filePersistenceService.PersistFileAsync(file.Stream, file.FileName);

                file.OnProcessed(file);
            }
        }
    }
}