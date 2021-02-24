using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<File> Files { get; private set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}