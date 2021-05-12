using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly DbSet<File> _set;
        public FileRepository(AppDbContext dbContext)
        {
            _set = dbContext.Set<File>();
        }

        public Task<File> GetByIdAsync(Guid id)
        {
            return _set.FirstOrDefaultAsync(f => f.Id == id);
        }

        public Task<File> GetWithUploaderIdAsync(Guid uploaderId, Guid fileId)
        {
            return _set.FirstOrDefaultAsync(f => f.UploaderId == uploaderId && f.Id == fileId);
        }

        public async Task<IReadOnlyList<File>> SearchAsync(ISpecification<File> criteria)
        {
            return (await _set.Where(criteria.Expression).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyList<File>> GetWithParentDirectoryAsync(Guid directoryId)
        {
            return (await _set.Where(f => f.DirectoryId == directoryId).ToListAsync()).AsReadOnly();
        }

        public async Task AddAsync(File file)
        {
            await _set.AddAsync(file);
        }
    }
}