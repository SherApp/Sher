using System;

namespace Sher.Core.Base
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