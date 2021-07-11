using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Files.CreateFile
{
    public class CreateFileCommandHandler : ICommandHandler<CreateFileCommand>
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IFileRepository _fileRepository;

        public CreateFileCommandHandler(
            IDirectoryRepository directoryRepository,
            IUploaderRepository uploaderRepository,
            IFileRepository fileRepository)
        {
            _uploaderRepository = uploaderRepository;
            _fileRepository = fileRepository;
            _directoryRepository = directoryRepository;
        }

        public async Task<Unit> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _uploaderRepository.GetByUserIdAsync(request.UserId);
            var directory = await _directoryRepository.GetWithOrRootAsync(request.DirectoryId, uploader.Id);

            var file = uploader.UploadFile(
                request.Id,
                directory.Id,
                request.FileName,
                request.FileLength);

            await _fileRepository.AddAsync(file);

            return Unit.Value;
        }
    }
}