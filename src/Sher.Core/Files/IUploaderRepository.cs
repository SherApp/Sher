using System;
using System.Threading.Tasks;

namespace Sher.Core.Files
{
    public interface IUploaderRepository
    {
        Task<Uploader> GetByIdAsync(Guid id);
    }
}