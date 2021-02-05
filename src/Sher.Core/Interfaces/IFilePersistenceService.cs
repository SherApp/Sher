using System.IO;
using System.Threading.Tasks;

namespace Sher.Core.Interfaces
{
    public interface IFilePersistenceService
    {
        Task PersistFileAsync(Stream fileStream, string fileName);
    }
}