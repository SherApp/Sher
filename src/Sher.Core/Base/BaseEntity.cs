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

        protected void CheckRule(IBusinessRule rule)
        {
            var error = rule.Check();
            if (error is not null)
            {
                throw new BusinessRuleViolationException(error);
            }
        }
    }
}