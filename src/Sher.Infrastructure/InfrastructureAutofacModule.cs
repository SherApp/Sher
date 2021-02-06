using System.Reflection;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Application.Interfaces;
using Sher.Core.Interfaces;
using Sher.Infrastructure.Data;
using Sher.Infrastructure.Data.Repositories;
using Module = Autofac.Module;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        private readonly Assembly _callingAssembly;

        public InfrastructureAutofacModule(Assembly callingAssembly = null)
        {
            _callingAssembly = callingAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IFileService).Assembly, typeof(AppDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAutoMapper(_callingAssembly, typeof(IFileService).Assembly);

            builder.RegisterMediatR(typeof(IRepository<>).Assembly);
            base.Load(builder);
        }
    }
}