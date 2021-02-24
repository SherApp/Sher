using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Application.Files.UploadFile
{
    public class FileProcessedNotificationHandler : INotificationHandler<FileProcessedNotification>
    {
        private readonly IRepository<File> _repository;

        public FileProcessedNotificationHandler(IRepository<File> repository)
        {
            _repository = repository;
        }

        public async Task Handle(FileProcessedNotification notification, CancellationToken cancellationToken)
        {
            var file = await _repository.GetByIdAsync(notification.Context.FileId);
            file.MarkAsProcessed();

            await _repository.UpdateAsync(file);
        }
    }
}