using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Processing;
using Sher.Core.Files;
using Sher.Core.Files.Uploaders;

namespace Sher.Application.Files.DeleteFile
{
    public class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand, bool>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUploaderRepository _uploaderRepository;

        public DeleteFileCommandHandler(IFileRepository fileRepository, IUploaderRepository uploaderRepository)
        {
            _fileRepository = fileRepository;
            _uploaderRepository = uploaderRepository;
        }

        public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var (fileId, userId) = request;
            var uploader = await _uploaderRepository.GetByUserIdAsync(userId);
            var file = await _fileRepository.GetWithUploaderIdAsync(uploader.Id, fileId);

            if (file is null)
            {
                return false;
            }

            file.Delete();
            return true;
        }
    }
}