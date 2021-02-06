using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Commands;
using Sher.Core;
using Sher.Core.Interfaces;
using File = Sher.Core.Entities.FileAggregate.File;

namespace Sher.Application.CommandHandlers
{
    public class FileCommandHandler : AsyncRequestHandler<FileUploadCommand>
    {
        private readonly IFileProcessingQueue<FileProcessingContext> _fileProcessingQueue;
        private readonly IRepository<File> _fileRepository;

        public FileCommandHandler(IFileProcessingQueue<FileProcessingContext> fileProcessingQueue, IRepository<File> fileRepository)
        {
            _fileProcessingQueue = fileProcessingQueue;
            _fileRepository = fileRepository;
        }
        
        protected override async Task Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var file = new File(request.Id, request.Slug, request.OriginalFileName);
            await _fileRepository.AddAsync(file);

            _fileProcessingQueue.QueueFile(request.FileStream, request.Slug, new FileProcessingContext { Id = request.Id });
        }
    }
}