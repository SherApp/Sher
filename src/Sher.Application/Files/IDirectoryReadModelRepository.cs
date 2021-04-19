using System;
using System.Threading.Tasks;

namespace Sher.Application.Files
{
    public interface IDirectoryReadModelRepository
    {
        public Task<DirectoryReadModel> GetWithAsync(Guid directoryId, Guid uploaderId);
        public Task AddAsync(DirectoryReadModel model);
    }
}