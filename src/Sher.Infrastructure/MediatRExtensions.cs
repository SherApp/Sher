using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Base;

namespace Sher.Infrastructure
{
    public static class MediatRExtensions
    {
        public static async Task PublishDomainEventsAsync(this IMediator mediator, DbContext ctx, CancellationToken cancellationToken = default)
        {
            var entries = ctx.ChangeTracker.Entries<BaseEntity>().ToList();
            foreach (var notification in entries.SelectMany(e => e.Entity.DomainEvents))
            {
                await mediator.Publish(notification, cancellationToken);
            }
        }
    }
}