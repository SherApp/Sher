using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSpecifications;

namespace Sher.Core.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(Guid id);

        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);

        public Task<IReadOnlyList<T>> ListAsync();

        public Task<T> FirstOrDefaultAsync(ISpecification<T> specification);

        public Task<T> AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task<T> RemoveAsync(T entity);
    }
}