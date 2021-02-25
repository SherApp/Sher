using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Base;
using File = Sher.Core.Files.File;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandHandler : AsyncRequestHandler<UploadFileCommand>
    {
        private readonly IFileProcessingQueue _fileProcessingQueue;
        private readonly IRepository<File> _fileRepository;

        public UploadFileCommandHandler(IFileProcessingQueue fileProcessingQueue, IRepository<File> fileRepository)
        {
            _fileProcessingQueue = fileProcessingQueue;
            _fileRepository = fileRepository;
        }
        
        protected override async Task Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var file = new File(request.Id, request.UploaderId, request.OriginalFileName);
            await _fileRepository.AddAsync(file);

            _fileProcessingQueue.QueueFile(request.FileStream, file.Slug, new FileProcessingContext(request.Id));
        }
    }
}