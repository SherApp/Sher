using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sher.Application.Files;
using Sher.Infrastructure.FileProcessing.Interfaces;
using Sher.SharedKernel.Options;

namespace Sher.Infrastructure.FileProcessing
{
    public class FilePersistenceService : IFilePersistenceService
    {
        private readonly FilePersistenceServiceOptions _options;

        public FilePersistenceService(IOptions<FilePersistenceServiceOptions> options)
        {
            _options = options.Value;
        }

        public async Task PersistFileAsync(Stream fileStream, string fileName)
        {
            var path = GetFilePath(fileName);
            var directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            fileStream.Position = 0;
            await using var stream = new FileStream(path, FileMode.Create);
            await fileStream.CopyToAsync(stream);
        }

        public bool DeleteFile(string fileName)
        {
            var path = GetFilePath(fileName);
            if (!File.Exists(path)) return false;
            
            File.Delete(path);

            return true;
        }

        private string GetFilePath(string fileName) =>
            Path.Combine(_options.UploadsDirectory, fileName);
    }
}