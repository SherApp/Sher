using MediatR;
using Sher.Core.Base;

namespace Sher.Application.Processing
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
    {
        
    }
}