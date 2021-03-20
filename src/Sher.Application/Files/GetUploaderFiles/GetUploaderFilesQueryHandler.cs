using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sher.Core.Files;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class GetUploaderFilesQueryHandler : IRequestHandler<GetUploaderFilesQuery, List<FileDto>>
    {
        private readonly IFileRepository _repository;
        private readonly IMapper _mapper;

        public GetUploaderFilesQueryHandler(IFileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> Handle(GetUploaderFilesQuery request, CancellationToken cancellationToken)
        {
            var (uploaderId, requiredFileNamePart) = request;
            var criteria = new FileSearchCriteria
            {
                UploaderId = uploaderId,
                RequiredFileNamePart = requiredFileNamePart
            };
            var files = await _repository.SearchAsync(criteria);
            return files.Select(f => _mapper.Map<FileDto>(f)).ToList();
        }
    }
}