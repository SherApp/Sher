using System.Collections.Generic;
using System.Threading.Tasks;
using Sher.Core.Files;

namespace Sher.Core.Base
{
    public interface IFileRepository : IRepository<File>
    {
        Task<IReadOnlyList<File>> GetFilesByUploaderId(string uploaderId);
    }
}