using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Files.GetUploaderFiles;
using Sher.Application.Processing;
using Sher.Core.Files;

namespace Sher.Application.Files.UploadFile
{
    public class UpdateReadModelFileUploadedHandler : IDomainEventHandler<FileUploadedEvent>
    {
        private readonly IDirectoryReadModelRepository _repository;

        public UpdateReadModelFileUploadedHandler(IDirectoryReadModelRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Handle(FileUploadedEvent notification, CancellationToken cancellationToken)
        {
            var directory = await _repository.GetWithAsync(notification.DirectoryId, notification.UploaderId);
            directory.Files.Add(new FileDto
            {
                FileName = notification.FileName,
                Id = notification.FileId,
                IsDeleted = false,
                Length = (int)notification.FileStream.Length
            });
        }
    }
}