using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sher.Application.Commands;
using Sher.Application.Interfaces;
using Sher.Application.Models;

namespace Sher.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileService(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public async Task UploadFile(UploadFileModel uploadFileModel)
        {
            await _mediator.Send(_mapper.Map<FileUploadCommand>(uploadFileModel));
        }
    }
}