using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Files;
using Sher.Core.Files.Directories;

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

        public Task<Directory> GetWithOrRootAsync(Guid? directoryId, Guid uploaderId)
        {
            if (directoryId is null)
            {
                return _dbSet.FirstOrDefaultAsync(Directory.IsRootFor(uploaderId).Expression);
            }

            return GetWithAsync(directoryId.Value, uploaderId);
        }

        public async Task AddAsync(Directory directory)
        {
            await _dbSet.AddAsync(directory);
        }
    }
}