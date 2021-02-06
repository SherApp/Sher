using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sher.Core.Commands;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;

namespace Sher.Core.CommandHandlers
{
    public class FileCommandHandler : AsyncRequestHandler<FileUploadCommand>
    {
        private readonly IServiceScopeFactory _serviceProvider;
        private readonly IFileProcessingQueue _fileProcessingQueue;

        public FileCommandHandler(
            IServiceScopeFactory serviceProvider,
            IFileProcessingQueue fileProcessingQueue)
        {
            _serviceProvider = serviceProvider;
            _fileProcessingQueue = fileProcessingQueue;
        }
        
        protected override Task Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            _fileProcessingQueue.QueueFile(request.FileStream, request.FileName, async _ =>
            {
                var file = new File(request.Id, request.FileName, request.OriginalFileName);

                using var scope = _serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<File>>();
                await repository.AddAsync(file);
            });

            return Task.CompletedTask;
        }
    }
}