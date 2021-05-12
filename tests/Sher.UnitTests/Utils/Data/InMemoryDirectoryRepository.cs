using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sher.Core.Files.Directories;

namespace Sher.UnitTests.Utils.Data
{
    public class InMemoryDirectoryRepository : IDirectoryRepository
    {
        private readonly List<Directory> _directories = new();

        public Task<Directory> GetWithAsync(Guid directoryId, Guid uploaderId)
        {
            return Task.FromResult(_directories.FirstOrDefault(d =>
                d.ParentDirectoryId == directoryId && d.UploaderId == uploaderId));
        }

        public Task AddAsync(Directory directory)
        {
            _directories.Add(directory);
            return Task.CompletedTask;
        }

        public Task<Directory> GetWithOrRootAsync(Guid? directoryId, Guid uploaderId)
        {
            if (directoryId.HasValue)
            {
                return GetWithAsync(directoryId.Value, uploaderId);
            }

            return Task.FromResult(_directories.FirstOrDefault(d => d.UploaderId == uploaderId));
        }

        public Task<IReadOnlyList<Directory>> GetWithParentAsync(Guid parentDirectoryId)
        {
            return Task.FromResult<IReadOnlyList<Directory>>(_directories
                .Where(d => d.ParentDirectoryId == parentDirectoryId).ToList().AsReadOnly());
        }
    }
}