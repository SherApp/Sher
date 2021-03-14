using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sher.Core.Base;

namespace Sher.Application.Processing
{
    public class DomainEventHandlerLoggingDecorator<TEvent> : IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        private readonly IDomainEventHandler<TEvent> _handler;
        private readonly ILogger<DomainEventHandlerLoggingDecorator<TEvent>> _logger;

        public DomainEventHandlerLoggingDecorator(
            IDomainEventHandler<TEvent> handler,
            ILogger<DomainEventHandlerLoggingDecorator<TEvent>> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public Task Handle(TEvent @event, CancellationToken cancellationToken)
        {
            try
            {
                return _handler.Handle(@event, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error handling Domain Event {Command}", typeof(TEvent).Name);
                throw;
            }
        }
    }
}