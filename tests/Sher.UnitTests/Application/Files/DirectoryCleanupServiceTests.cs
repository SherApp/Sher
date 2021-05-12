using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Sher.Application.Files.DeleteDirectory;
using Sher.Core.Base;
using Sher.Core.Files;
using Sher.Core.Files.Directories;
using Sher.UnitTests.Builders;
using Sher.UnitTests.Utils.Data;
using Xunit;

namespace Sher.UnitTests.Application.Files
{
    public class DirectoryCleanupBackgroundServiceTests
    {
        [Fact]
        public async Task DirectoryCleanupBackgroundService_PerformsCleanup()
        {
            // Arrange
            var taskRepo = new InMemoryDirectoryCleanupTaskRepository();
            var directoryRepo = new InMemoryDirectoryRepository();
            var fileRepo = new InMemoryFileRepository();

            var scopeFactory = GetServiceScopeFactory(taskRepo, directoryRepo, fileRepo);

            var directory = new DirectoryBuilder().Build();
            var childDir = new DirectoryBuilder().WithParentDirectoryId(directory.Id).Build();
            var file = new FileBuilder().WithDirectoryId(directory.Id).Build();

            await directoryRepo.AddAsync(directory);
            await directoryRepo.AddAsync(childDir);
            await fileRepo.AddAsync(file);

            var task = new DirectoryCleanupTask(directory.Id);
            await taskRepo.AddAsync(task);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(1000);

            var service = new DirectoryCleanupService(scopeFactory, Mock.Of<ILogger<DirectoryCleanupService>>());
            
            // Act
            await service.StartAsync(cts.Token);

            // Assert
            Assert.True(childDir.IsDeleted);
            Assert.True(file.IsDeleted);
            Assert.True(task.IsProcessed);
        }

        private static IServiceScopeFactory GetServiceScopeFactory(
            IDirectoryCleanupTaskRepository taskRepo,
            IDirectoryRepository dirRepo,
            IFileRepository fileRepo)
        {
            var scope = Mock.Of<IServiceScope>(s =>
                s.ServiceProvider.GetService(typeof(IDirectoryCleanupTaskRepository)) == taskRepo &&
                s.ServiceProvider.GetService(typeof(IDirectoryRepository)) == dirRepo &&
                s.ServiceProvider.GetService(typeof(IFileRepository)) == fileRepo && 
                s.ServiceProvider.GetService(typeof(IUnitOfWork)) == Mock.Of<IUnitOfWork>());
            
            return Mock.Of<IServiceScopeFactory>(s => s.CreateScope() == scope);
        }
    }
}