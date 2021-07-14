using System;
using System.Threading;
using System.Threading.Tasks;
using Sher.Core.Access.Users;
using Sher.Core.Base;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Access.RegisterUser
{
    public class CreateUploaderUserCreatedEventHandler : IDomainEventHandler<UserRegisteredEvent>
    {
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IDirectoryRepository _directoryRepository;

        public CreateUploaderUserCreatedEventHandler(
            IUploaderRepository uploaderRepository,
            IDirectoryRepository directoryRepository)
        {
            _uploaderRepository = uploaderRepository;
            _directoryRepository = directoryRepository;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var uploader = new Uploader(Guid.NewGuid(), notification.UserId);
            var rootDirectory = new Directory(Guid.NewGuid(), null, uploader.Id, "Root");

            await _uploaderRepository.AddAsync(uploader);
            await _directoryRepository.AddAsync(rootDirectory);
        }
    }
}