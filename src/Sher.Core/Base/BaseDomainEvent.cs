using System;

namespace Sher.Core.Base
{
    public class BaseDomainEvent : IDomainEvent
    {
        public DateTime OccurrenceDate { get; } = DateTime.Now;
    }
}