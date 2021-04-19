using System;
using System.Threading.Tasks;

namespace Sher.Core.Files
{
    public interface IDirectoryRepository
    {
        public Task<Directory> GetWithAsync(Guid directoryId, Guid uploaderId);
        public Task AddAsync(Directory directory);
    }
}