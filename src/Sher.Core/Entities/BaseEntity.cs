using System;

namespace Sher.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public bool IsDeleted { get; private set; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}