using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using Sher.Core.Base;

namespace Sher.Infrastructure.Data.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        protected DbSet<T> Set => _dbContext.Set<T>();

        public EfRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Set.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await Set.Where(specification.Expression).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            return await Set.FirstOrDefaultAsync(specification.Expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await Set.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> RemoveAsync(T entity)
        {
            var result = Set.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}