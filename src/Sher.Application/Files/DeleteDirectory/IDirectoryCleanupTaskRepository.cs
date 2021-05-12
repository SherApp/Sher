using System.Threading.Tasks;

namespace Sher.Application.Files.DeleteDirectory
{
    public interface IDirectoryCleanupTaskRepository
    {
        Task<DirectoryCleanupTask> GetFirstUnprocessedAsync();
        Task AddAsync(DirectoryCleanupTask task);
    }
}