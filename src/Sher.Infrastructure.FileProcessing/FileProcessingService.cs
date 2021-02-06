using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sher.Core.Interfaces;
using Sher.Core.Notifications;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingService<TContext> : BackgroundService
    {
        private readonly IFileQueue<TContext> _queue;
        private readonly IFilePersistenceService _filePersistenceService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public FileProcessingService(IFileQueue<TContext> queue, IFilePersistenceService filePersistenceService, IServiceScopeFactory serviceScopeFactory)
        {
            _queue = queue;
            _filePersistenceService = filePersistenceService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var file = await _queue.DequeueFileAsync(stoppingToken);

                // TODO: Create a file processing pipeline (eg. compress images)

                await _filePersistenceService.PersistFileAsync(file.Stream, file.FileName);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Publish(new FileProcessedNotification<TContext>(file.Context), stoppingToken);
                }

                await file.Stream.DisposeAsync();
            }
        }
    }
}