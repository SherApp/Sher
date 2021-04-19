using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Application.Files;

namespace Sher.Infrastructure.Data.Repositories
{
    public class DirectoryReadModelRepository : IDirectoryReadModelRepository
    {
        private readonly DbSet<DirectoryReadModel> _dbSet;

        public DirectoryReadModelRepository(AppDbContext dbContext)
        {
            _dbSet = dbContext.Set<DirectoryReadModel>();
        }

        public Task<DirectoryReadModel> GetWithAsync(Guid directoryId, Guid uploaderId)
        {
            return _dbSet.FirstOrDefaultAsync(d => d.Id == directoryId && d.UploaderId == uploaderId);
        }

        public async Task AddAsync(DirectoryReadModel model)
        {
            await _dbSet.AddAsync(model);
        }
    }
}