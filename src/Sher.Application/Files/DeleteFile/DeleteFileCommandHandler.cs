using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Application.Files.DeleteFile
{
    public class DeleteFileCommandHandler : AsyncRequestHandler<DeleteFileCommand>
    {
        private readonly IRepository<File> _fileRepository;

        public DeleteFileCommandHandler(IRepository<File> fileRepository)
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