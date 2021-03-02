using System.Linq;
using System.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sher.Core.Base;

namespace Sher.Infrastructure
{
    public static class MediatRExtensions
    {
        public static void PublishDomainEvents(this IMediator mediator, DbContext ctx, CancellationToken cancellationToken = default)
        {
            var entries = ctx.ChangeTracker.Entries<BaseEntity>();
            foreach (var notification in entries.SelectMany(e => e.Entity.DomainEvents))
            {
                mediator.Publish(notification, cancellationToken);
            }
        }
    }
}