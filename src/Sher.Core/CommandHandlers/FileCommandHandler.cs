using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Commands;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;

namespace Sher.Core.CommandHandlers
{
    public class FileCommandHandler : AsyncRequestHandler<FileUploadCommand>
    {
        private readonly IRepository<File> _repository;

        public FileCommandHandler(IRepository<File> repository)
        {
            _repository = repository;
        }
        
        protected override async Task Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var file = new File(request.Id, request.FileName, request.OriginalFileName);
            await _repository.AddAsync(file);
        }
    }
}