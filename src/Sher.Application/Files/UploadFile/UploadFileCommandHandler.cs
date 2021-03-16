using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Files;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IUploaderRepository _uploaderRepository;
        private readonly IFileRepository _fileRepository;

        public UploadFileCommandHandler(
            IUploaderRepository uploaderRepository,
            IFileRepository fileRepository)
        {
            _uploaderRepository = uploaderRepository;
            _fileRepository = fileRepository;
        }
        
        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _uploaderRepository.GetByIdAsync(request.UploaderId);

            var file = uploader.UploadFile(
                request.Id,
                request.FileName,
                request.FileStream.Length,
                request.FileStream);

            await _fileRepository.AddAsync(file);

            return Unit.Value;
        }
    }
}