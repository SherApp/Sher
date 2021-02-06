using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Notifications;
using Sher.Core;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;

namespace Sher.Application.NotificationHandlers
{
    public class FileNotificationHandler : INotificationHandler<FileProcessedNotification<FileProcessingContext>>
    {
        private readonly IRepository<File> _repository;

        public FileNotificationHandler(IRepository<File> repository)
        {
            _repository = repository;
        }

        public async Task Handle(FileProcessedNotification<FileProcessingContext> notification, CancellationToken cancellationToken)
        {
            var file = await _repository.GetByIdAsync(notification.Context.Id);
            file.MarkAsProcessed();

            await _repository.UpdateAsync(file);
        }
    }
}