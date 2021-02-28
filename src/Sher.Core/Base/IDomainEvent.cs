using System;
using MediatR;

namespace Sher.Core.Base
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurrenceDate { get; }
    }
}