using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Base;

namespace Sher.Application.Files.DeleteFile
{
    public class DeleteFileCommandHandler : AsyncRequestHandler<DeleteFileCommand>
    {
        private readonly IFileRepository _fileRepository;

        public DeleteFileCommandHandler(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        protected override async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var (fileId, userId) = request;
            var file = await _fileRepository.GetByIdAsync(fileId);

            if (file.UploaderId != userId)
                throw new ArgumentException("File can only be deleted by its uploader.");

            file.Delete();
            await _fileRepository.UpdateAsync(file);
        }
    }
}