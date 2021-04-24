using MediatR;

namespace Sher.Core.Base
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
        
    }
}