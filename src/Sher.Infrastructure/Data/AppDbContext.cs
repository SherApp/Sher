using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Base;
using Sher.Core.Files;

namespace Sher.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
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

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var notification in entries.SelectMany(e => e.Entity.DomainEvents))
            {
                _mediator.Publish(notification, cancellationToken);
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}