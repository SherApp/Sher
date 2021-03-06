using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Sher.Application.Files;
using Sher.SharedKernel.Options;

namespace Sher.Infrastructure.FileProcessing
{
    public class FilePersistenceService : IFilePersistenceService
    {
        private readonly IFileSystem _fileSystem;
        private readonly FilePersistenceServiceOptions _options;

        public FilePersistenceService(IOptions<FilePersistenceServiceOptions> options, IFileSystem fileSystem = null)
        {
            _options = options.Value;
            _fileSystem = fileSystem ?? new FileSystem();
        }

        public async Task PersistFileAsync(Stream fileStream, string fileName)
        {
            var path = GetFilePath(fileName);
            var directory = Path.GetDirectoryName(path);

            if (!_fileSystem.Directory.Exists(directory))
            {
                _fileSystem.Directory.CreateDirectory(directory);
            }

            fileStream.Position = 0;
            await using var stream = _fileSystem.FileStream.Create(path, FileMode.Create);
            await fileStream.CopyToAsync(stream);
        }

        public bool DeleteFileDirectory(string directoryName)
        {
            var path = GetFilePath(directoryName);
            if (!_fileSystem.Directory.Exists(path)) return false;
            
            _fileSystem.Directory.Delete(path, true);

            return true;
        }

        private string GetFilePath(string fileName) =>
            Path.Combine(_options.UploadsDirectory, fileName);
    }
}