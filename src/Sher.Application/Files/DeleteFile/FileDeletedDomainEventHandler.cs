using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sher.Core.Base;
using Sher.Core.Files;
using File = System.IO.File;

namespace Sher.Application.Files.DeleteFile
{
    public class FileDeletedDomainEventHandler : IDomainEventHandler<FileDeletedEvent>
    {
        private readonly IUploaderFileStorePathProvider _fileStorePathProvider;

        public FileDeletedDomainEventHandler(IUploaderFileStorePathProvider fileStorePathProvider)
        {
            _fileStorePathProvider = fileStorePathProvider;
        }

        public Task Handle(FileDeletedEvent notification, CancellationToken cancellationToken)
        {
            var storePath = _fileStorePathProvider
                .GetOrCreateFileStorePathForUploaderOfId(notification.UploaderId.ToString());

            var filePath = Path.Combine(storePath, notification.FileId.ToString("N"));

            File.Delete(filePath);
            
            return Task.CompletedTask;
        }
    }
}