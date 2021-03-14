using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Files;

namespace Sher.Application.Files.DeleteFile
{
    public class DeleteFileCommandHandler : AsyncRequestHandler<DeleteFileCommand>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUploaderRepository _uploaderRepository;

        public DeleteFileCommandHandler(IFileRepository fileRepository, IUploaderRepository uploaderRepository)
        {
            _fileRepository = fileRepository;
            _uploaderRepository = uploaderRepository;
        }

        protected override async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var (fileId, uploaderId) = request;
            var uploader = await _uploaderRepository.GetByIdAsync(uploaderId);
            var file = await _fileRepository.GetByIdAsync(fileId);

            uploader.DeleteFile(file);
        }
    }
}