using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Directories;

namespace Sher.Application.Files.CreateDirectory
{
    public class UpdateReadModelDirectoryCreatedEventHandler : IDomainEventHandler<DirectoryCreatedEvent>
    {
        private readonly IDirectoryReadModelRepository _repository;

        public UpdateReadModelDirectoryCreatedEventHandler(IDirectoryReadModelRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Handle(DirectoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            var directory = new DirectoryReadModel(
                notification.DirectoryId,
                notification.UploaderId,
                notification.Name);

            await _repository.AddAsync(directory);

            if (!notification.ParentDirectoryId.HasValue)
            {
                return;
            }

            var parentDirectory = await _repository.GetWithAsync(
                notification.ParentDirectoryId.Value,
                notification.UploaderId);

            parentDirectory.Directories.Add(directory);
        }
    }
}