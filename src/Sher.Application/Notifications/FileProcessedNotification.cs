using MediatR;

namespace Sher.Application.Notifications
{
    public class FileProcessedNotification<TContext> : INotification
    {
        public TContext Context { get; set; }

        public FileProcessedNotification(TContext context)
        {
            Context = context;
        }
    }
}