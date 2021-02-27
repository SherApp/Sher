using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data.Repositories
{
    public class FileRepository : EfRepository<File>, IFileRepository
    {
        public FileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<File>> GetFilesByUploaderId(string uploaderId)
        {
            return await Set.Where(e => e.UploaderId == uploaderId).ToListAsync();
        }
    }
}