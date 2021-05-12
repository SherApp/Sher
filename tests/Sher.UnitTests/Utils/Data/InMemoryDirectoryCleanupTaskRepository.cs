using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sher.Application.Files.DeleteDirectory;

namespace Sher.UnitTests.Utils.Data
{
    public class InMemoryDirectoryCleanupTaskRepository : IDirectoryCleanupTaskRepository
    {
        private readonly List<DirectoryCleanupTask> _tasks = new();

        public Task<DirectoryCleanupTask> GetFirstUnprocessedAsync()
        {
            return Task.FromResult(_tasks.FirstOrDefault(t => !t.IsProcessed));
        }

        public Task AddAsync(DirectoryCleanupTask task)
        {
            _tasks.Add(task);
            return Task.CompletedTask;
        }
    }
}