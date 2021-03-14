using System.Collections.Generic;

namespace Sher.Core.Base
{
    
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; protected set; }

        internal IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<IDomainEvent> _domainEvents = new();

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }
    }
}