using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Notifications;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;

namespace Sher.Application.NotificationHandlers
{
    public class FileNotificationHandler : INotificationHandler<FileProcessedNotification>
    {
        private readonly IRepository<File> _repository;

        public FileNotificationHandler(IRepository<File> repository)
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