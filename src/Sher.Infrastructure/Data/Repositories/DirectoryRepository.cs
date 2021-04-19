using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Repositories
{
    public class DirectoryRepository : IDirectoryRepository
    {
        private readonly DbSet<Directory> _dbSet;

        public DirectoryRepository(AppDbContext dbContext)
        {
            _dbSet = dbContext.Set<Directory>();
        }

        public Task<Directory> GetWithAsync(Guid directoryId, Guid uploaderId)
        {
            return _dbSet.FirstOrDefaultAsync(d => d.Id == directoryId && d.UploaderId == uploaderId);
        }

        public async Task AddAsync(Directory directory)
        {
            await _dbSet.AddAsync(directory);
        }
    }
}