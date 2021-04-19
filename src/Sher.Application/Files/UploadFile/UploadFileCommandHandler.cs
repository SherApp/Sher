using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Directories;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IFileRepository _fileRepository;

        public UploadFileCommandHandler(
            IDirectoryRepository directoryRepository,
            IFileRepository fileRepository)
        {
            _directoryRepository = directoryRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var directory = await _directoryRepository.GetWithAsync(request.DirectoryId, request.UploaderId);

            var file = directory.UploadFile(
                request.Id,
                request.FileName,
                request.FileStream.Length,
                request.FileStream);

            await _fileRepository.AddAsync(file);

            return Unit.Value;
        }
    }
}