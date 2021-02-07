using System.IO;
using System.Threading.Tasks;

namespace Sher.Infrastructure.FileProcessing.Interfaces
{
    public interface IFilePersistenceService
    {
        Task PersistFileAsync(Stream fileStream, string fileName);
    }
}