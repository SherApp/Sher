using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Files;

namespace Sher.Application.Files.UploadFile
{
    public class FileProcessedNotificationHandler : INotificationHandler<FileProcessedNotification>
    {
        private readonly IFileRepository _repository;

        public FileProcessedNotificationHandler(IFileRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(FileProcessedNotification notification, CancellationToken cancellationToken)
        {
            var file = await _repository.GetByIdAsync(notification.Context.FileId);
            file.MarkAsProcessed();
        }
    }
}