using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Application.Files.UploadFile
{
    public class CopyFileUploadedDomainEventHandler : IDomainEventHandler<FileUploadedEvent>
    {
        private readonly IFileProcessingQueue _fileProcessingQueue;

        public CopyFileUploadedDomainEventHandler(IFileProcessingQueue fileProcessingQueue)
        {
            _fileProcessingQueue = fileProcessingQueue;
        }

        public Task Handle(FileUploadedEvent notification, CancellationToken cancellationToken)
        {
            var fullFileName = Path.Combine(notification.FileId.ToString(), notification.FileName);
            _fileProcessingQueue.QueueFile(
                notification.FileStream,
                fullFileName,
                new FileProcessingContext(notification.FileId));

            return Task.CompletedTask;
        }
    }
}