using System.IO;
using System.Threading.Tasks;

namespace Sher.Application.Files
{
    public interface IFilePersistenceService
    {
        Task PersistFileAsync(Stream fileStream, string fileName);
        bool DeleteFileDirectory(string directoryName);
    }
}