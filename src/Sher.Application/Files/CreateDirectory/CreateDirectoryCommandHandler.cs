using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files;
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
            var uploader = await _uploaderRepository.GetByIdAsync(request.UploaderId);
            var directory = uploader.CreateDirectory(request.DirectoryId, request.ParentDirectoryId, request.Name);

            await _directoryRepository.AddAsync(directory);
            
            return Unit.Value;
        }
    }
}