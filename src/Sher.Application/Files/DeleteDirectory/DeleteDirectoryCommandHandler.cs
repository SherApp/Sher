using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Files.DeleteDirectory
{
    public class DeleteDirectoryCommandHandler : ICommandHandler<DeleteDirectoryCommand>
    {
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IDirectoryRepository _directoryRepository;

        public DeleteDirectoryCommandHandler(
            IUploaderRepository uploaderRepository,
            IDirectoryRepository directoryRepository)
        {
            _uploaderRepository = uploaderRepository;
            _directoryRepository = directoryRepository;
        }

        public async Task<Unit> Handle(DeleteDirectoryCommand request, CancellationToken cancellationToken)
        {
            var (directoryId, userId) = request;
            var uploader = await _uploaderRepository.GetByUserIdAsync(userId);
            var directory = await _directoryRepository.GetWithAsync(directoryId, uploader.Id);

            directory.Delete();
            
            return Unit.Value;
        }
    }
}