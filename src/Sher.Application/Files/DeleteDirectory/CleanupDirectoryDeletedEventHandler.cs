using System.Threading;
using System.Threading.Tasks;
using Sher.Core.Base;
using Sher.Core.Files.Directories;

namespace Sher.Application.Files.DeleteDirectory
{
    public class CleanupDirectoryDeletedEventHandler : IDomainEventHandler<DirectoryDeletedEvent>
    {
        private readonly IDirectoryCleanupTaskRepository _repository;

        public CleanupDirectoryDeletedEventHandler(IDirectoryCleanupTaskRepository directoryCleanupTaskRepository)
        {
            _repository = directoryCleanupTaskRepository;
        }

        public async Task Handle(DirectoryDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new DirectoryCleanupTask(notification.DirectoryId));
        }
    }
}