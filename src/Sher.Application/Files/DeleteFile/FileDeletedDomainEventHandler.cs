using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Events;

namespace Sher.Application.Files.DeleteFile
{
    public class FileDeletedDomainEventHandler : IDomainEventHandler<FileDeletedEvent>
    {
        private readonly IFilePersistenceService _filePersistenceService;

        public FileDeletedDomainEventHandler(IFilePersistenceService filePersistenceService)
        {
            _filePersistenceService = filePersistenceService;
        }

        public Task Handle(FileDeletedEvent notification, CancellationToken cancellationToken)
        {
            _filePersistenceService.DeleteFileDirectory(notification.FileId.ToString());

            return Task.CompletedTask;
        }
    }
}