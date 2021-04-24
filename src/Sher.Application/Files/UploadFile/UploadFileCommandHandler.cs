using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Directories;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IFileRepository _fileRepository;

        public UploadFileCommandHandler(
            IDirectoryRepository directoryRepository,
            IUploaderRepository uploaderRepository,
            IFileRepository fileRepository)
        {
            _directoryRepository = directoryRepository;
            _uploaderRepository = uploaderRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _uploaderRepository.GetByUserIdAsync(request.UserId);
            var directory = await _directoryRepository.GetWithOrRootAsync(request.DirectoryId, uploader.Id);

            var file = uploader.UploadFile(
                request.Id,
                directory.Id,
                request.FileName,
                request.FileStream.Length,
                request.FileStream);

            await _fileRepository.AddAsync(file);

            return Unit.Value;
        }
    }
}