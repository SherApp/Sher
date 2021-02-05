using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Application.Interfaces;
using Sher.Core.Interfaces;
using Sher.Infrastructure.Data;
using Sher.Infrastructure.Data.Repositories;
using Module = Autofac.Module;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IFileService).Assembly, typeof(AppDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterMediatR(typeof(IRepository<>).Assembly);
            base.Load(builder);
        }
    }
}