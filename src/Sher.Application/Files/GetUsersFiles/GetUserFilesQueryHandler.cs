using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Application.Files.GetUsersFiles
{
    public class GetUserFilesQueryHandler : IRequestHandler<GetUserFilesQuery, List<FileDto>>
    {
        private readonly IRepository<File> _repository;
        private readonly IMapper _mapper;

        public GetUserFilesQueryHandler(IRepository<File> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
        {
            var (userId, requiredFileNamePart) = request;
            var criteria = new FileSearchCriteria
            {
                UploaderId = userId,
                RequiredFileNamePart = requiredFileNamePart
            };
            var files = await _repository.ListAsync(criteria);
            return files.Select(f => _mapper.Map<FileDto>(f)).ToList();
        }
    }
}