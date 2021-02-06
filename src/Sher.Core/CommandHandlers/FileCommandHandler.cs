using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sher.Core.Commands;
using Sher.Core.Interfaces;
using File = Sher.Core.Entities.FileAggregate.File;

namespace Sher.Core.CommandHandlers
{
    public class FileCommandHandler : AsyncRequestHandler<FileUploadCommand>
    {
        private readonly IFileProcessingQueue _fileProcessingQueue;

        public FileCommandHandler(IFileProcessingQueue fileProcessingQueue)
        {
            _fileProcessingQueue = fileProcessingQueue;
        }
        
        protected override Task Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            _fileProcessingQueue.QueueFile(request.FileStream, request.Slug, async scope =>
            {
                var file = new File(request.Id, request.Slug, request.OriginalFileName);

                var repository = scope.ServiceProvider.GetRequiredService<IRepository<File>>();
                await repository.AddAsync(file);
            });

            return Task.CompletedTask;
        }
    }
}