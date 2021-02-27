using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sher.Core.Base;

namespace Sher.Application.Files.GetUsersFiles
{
    public class GetUserFilesQueryHandler : IRequestHandler<GetUserFilesQuery, List<FileDto>>
    {
        private readonly IFileRepository _repository;
        private readonly IMapper _mapper;

        public GetUserFilesQueryHandler(IFileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _repository.GetFilesByUploaderId(request.UserId);
            return files.Select(f => _mapper.Map<FileDto>(f)).ToList();
        }
    }
}