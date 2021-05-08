using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Files.CreateDirectory
{
    public class CreateDirectoryCommandHandler : ICommandHandler<CreateDirectoryCommand>
    {
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IDirectoryRepository _directoryRepository;

        public CreateDirectoryCommandHandler(
            IUploaderRepository uploaderRepository,
            IDirectoryRepository directoryRepository)
        {
            _uploaderRepository = uploaderRepository;
            _directoryRepository = directoryRepository;
        }
        
        public async Task<Unit> Handle(CreateDirectoryCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _uploaderRepository.GetByUserIdAsync(request.UserId);
            var parentDirectory = await _directoryRepository.GetWithOrRootAsync(request.ParentDirectoryId, uploader.Id);

            var directory = parentDirectory.CreateChildDirectory(request.DirectoryId, request.Name);

            await _directoryRepository.AddAsync(directory);
            
            return Unit.Value;
        }
    }
}