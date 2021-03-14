using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Base;
using Sher.Core.Files;
using File = Sher.Core.Files.File;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IUploaderRepository _uploaderRepository;

        public UploadFileCommandHandler(IUploaderRepository uploaderRepository)
        {
            _uploaderRepository = uploaderRepository;
        }
        
        public async Task<Unit> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var uploader = await _uploaderRepository.GetByIdAsync(request.UploaderId);
            uploader.UploadFile(request.Id, request.FileName, request.FileStream.Length, request.FileStream);

            return Unit.Value;
        }
    }
}