using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Commands;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;

namespace Sher.Core.CommandHandlers
{
    public class FileCommandHandler : IRequestHandler<FileUploadCommand>
    {
        private readonly IRepository<File> _repository;
        private readonly IFilePersistenceService _filePersistenceService;

        public FileCommandHandler(
            IRepository<File> repository,
            IFilePersistenceService filePersistenceService)
        {
            _repository = repository;
            _filePersistenceService = filePersistenceService;
        }
        
        public async Task<Unit> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            await _filePersistenceService.PersistFileAsync(request.FileStream, request.FileName);

            var file = new File(request.Id, request.FileName, request.OriginalFileName);
            await _repository.AddAsync(file);

            return default;
        }
    }
}