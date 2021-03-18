using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Access.Users;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Uploader> Uploaders { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<File> Files { get; private set; }

        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public AppDbContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            await _mediator.PublishDomainEventsAsync(this, cancellationToken);

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}