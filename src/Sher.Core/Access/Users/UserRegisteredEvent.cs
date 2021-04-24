using System;
using Sher.Core.Base;

namespace Sher.Core.Access.Users
{
    public class UserRegisteredEvent : BaseDomainEvent
    {
        public Guid UserId { get; }

        public UserRegisteredEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}