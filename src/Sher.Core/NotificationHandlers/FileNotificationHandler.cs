using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Core.Entities.FileAggregate;
using Sher.Core.Interfaces;
using Sher.Core.Notifications;

namespace Sher.Core.NotificationHandlers
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
        }
    }
}