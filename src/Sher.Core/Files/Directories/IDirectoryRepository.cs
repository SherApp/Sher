using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sher.Core.Files.Directories
{
    public interface IDirectoryRepository
    {
        public Task<Directory> GetWithAsync(Guid directoryId, Guid uploaderId);
        public Task AddAsync(Directory directory);
        public Task<Directory> GetWithOrRootAsync(Guid? directoryId, Guid uploaderId);
        public Task<IReadOnlyList<Directory>> GetWithParentAsync(Guid parentDirectoryId);
    }
}