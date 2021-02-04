using MediatR;
using Sher.Application.Interfaces;
using Sher.Application.Models;
using Sher.Core.Commands;

namespace Sher.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IMediator _mediator;

        public FileService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public void UploadFile(UploadFileModel uploadFileModel)
        {
            _mediator.Publish(new FileUploadCommand(uploadFileModel.Id, uploadFileModel.Id.ToString(),
                uploadFileModel.File.FileName, uploadFileModel.File.Stream));
        }
    }
}