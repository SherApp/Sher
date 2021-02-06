using MediatR;

namespace Sher.Core.Notifications
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