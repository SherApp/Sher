using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Sher.Application.Files.ListDirectory
{
    public class ListDirectoryQueryHandler : IRequestHandler<ListDirectoryQuery, DirectoryReadModel>
    {
        private readonly IDirectoryReadModelRepository _repository;

        public ListDirectoryQueryHandler(IDirectoryReadModelRepository repository)
        {
            _repository = repository;
        }
        
        public Task<DirectoryReadModel> Handle(ListDirectoryQuery request, CancellationToken cancellationToken)
        {
            var (directoryId, uploaderId) = request;
            return _repository.GetWithAsync(directoryId, uploaderId);
        }
    }
}