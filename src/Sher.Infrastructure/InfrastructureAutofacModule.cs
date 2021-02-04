using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Core.Interfaces;
using Sher.Infrastructure.Data.Repositories;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(IRepository<>).Assembly);
            base.Load(builder);
        }
    }
}