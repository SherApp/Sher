using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Access;

namespace Sher.Infrastructure.Data.Repositories
{
    public class PlatformInstanceRepository : IPlatformInstanceRepository
    {
        private readonly DbSet<PlatformInstance> _set;

        public PlatformInstanceRepository(AppDbContext dbContext)
        {
            _set = dbContext.Set<PlatformInstance>();
        }
        
        public Task<PlatformInstance> GetPlatformInstance()
        {
            return _set.FirstOrDefaultAsync();
        }

        public async Task SetupInstance(PlatformInstance instance)
        {
            var existingPlatform = await GetPlatformInstance();
            if (existingPlatform is not null)
            {
                throw new InvalidOperationException(
                    $"PlatformInstance is a singleton. Update its properties instead of calling {nameof(SetupInstance)}");
            }

            await _set.AddAsync(instance);
        }
    }
}