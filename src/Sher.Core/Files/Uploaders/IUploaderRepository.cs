using System;
using System.Threading.Tasks;

namespace Sher.Core.Files.Uploaders
{
    public interface IUploaderRepository
    {
        Task<Uploader> GetByIdAsync(Guid id);
        Task AddAsync(Uploader uploader);
        Task<Uploader> GetByUserIdAsync(Guid userId);
    }
}