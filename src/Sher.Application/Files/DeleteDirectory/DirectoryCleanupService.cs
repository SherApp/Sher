using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sher.Core.Base;
using Sher.Core.Files;
using Sher.Core.Files.Directories;

namespace Sher.Application.Files.DeleteDirectory
{
    public class DirectoryCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<DirectoryCleanupService> _logger;

        public DirectoryCleanupService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DirectoryCleanupService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                var directoryCleanupTaskRepository = serviceProvider.GetRequiredService<IDirectoryCleanupTaskRepository>();

                var task = await directoryCleanupTaskRepository.GetFirstUnprocessedAsync();

                if (task is null)
                {
                    await Task.Delay(5000, stoppingToken);
                    continue;
                }

                try
                {
                    await DoCleanupAsync(task, scope.ServiceProvider);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Couldn't cleanup directory {DirectoryId}", task.DirectoryId);
                }
            }
        }

        private async Task DoCleanupAsync(DirectoryCleanupTask task, IServiceProvider provider)
        {
            _logger.LogTrace("Performing cleanup on directory {DirectoryId}", task.DirectoryId);

            var fileRepo = provider.GetRequiredService<IFileRepository>();
            var directoryRepo = provider.GetRequiredService<IDirectoryRepository>();

            var descendantDirs = await directoryRepo.GetWithParentAsync(task.DirectoryId);
            var descendantFiles = await fileRepo.GetWithParentDirectoryAsync(task.DirectoryId);

            foreach (var directory in descendantDirs.Where(d => !d.IsDeleted))
            {
                directory.Delete();
            }

            foreach (var file in descendantFiles.Where(f => !f.IsDeleted))
            {
                file.Delete();
            }

            task.IsProcessed = true;

            var unitOfWork = provider.GetRequiredService<IUnitOfWork>();
            await unitOfWork.CommitChangesAsync();
            
            _logger.LogTrace("Successful cleanup on {DirectoryId}", task.DirectoryId);
        }
    }
}