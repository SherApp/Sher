using MediatR;
using Sher.SharedKernel;

namespace Sher.Application.Notifications
{
    public class FileProcessedNotification : INotification
    {
        public IFileProcessingContext Context { get; set; }

        public FileProcessedNotification(IFileProcessingContext context)
        {
            Context = context;
        }
    }
}