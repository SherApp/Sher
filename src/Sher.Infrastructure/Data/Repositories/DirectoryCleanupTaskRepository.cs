using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Application.Files.DeleteDirectory;

namespace Sher.Infrastructure.Data.Repositories
{
    public class DirectoryCleanupTaskRepository : IDirectoryCleanupTaskRepository
    {
        private readonly DbSet<DirectoryCleanupTask> _directoryCleanupTasks;

        public DirectoryCleanupTaskRepository(AppDbContext dbContext)
        {
            _directoryCleanupTasks = dbContext.Set<DirectoryCleanupTask>();
        }

        public Task<DirectoryCleanupTask> GetFirstUnprocessedAsync()
        {
            return _directoryCleanupTasks.FirstOrDefaultAsync(d => d.IsProcessed == false);
        }

        public async Task AddAsync(DirectoryCleanupTask task)
        {
            await _directoryCleanupTasks.AddAsync(task);
        }
    }
}