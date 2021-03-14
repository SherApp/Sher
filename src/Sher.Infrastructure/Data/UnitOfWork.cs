using System.Threading.Tasks;
using Sher.Core.Base;

namespace Sher.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CommitChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}