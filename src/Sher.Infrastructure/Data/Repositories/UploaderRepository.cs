using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Repositories
{
    public class UploaderRepository : IUploaderRepository
    {
        private readonly DbSet<Uploader> _uploaders;

        public UploaderRepository(AppDbContext dbContext)
        {
            _uploaders = dbContext.Set<Uploader>();
        }

        public Task<Uploader> GetByIdAsync(Guid id)
        {
            return _uploaders.FirstOrDefaultAsync(u => u.Id.Value == id);
        }
    }
}