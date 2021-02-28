using System;
using System.Collections.Generic;

namespace Sher.Core.Base
{
    
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public bool IsDeleted { get; protected set; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        internal IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly List<IDomainEvent> _domainEvents = new();

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }
    }
}