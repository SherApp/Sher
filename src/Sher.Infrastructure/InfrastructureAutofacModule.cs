using System.Reflection;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Application.Interfaces;
using Sher.Core.Interfaces;
using Sher.Infrastructure.Data;
using Sher.Infrastructure.Data.Repositories;
using Module = Autofac.Module;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Sher.Infrastructure.FileProcessing.Interfaces;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        private readonly Assembly _callingAssembly;
        private readonly Assembly[] _serviceAssemblies;

        public InfrastructureAutofacModule(Assembly callingAssembly = null)
        {
            _callingAssembly = callingAssembly;
            _serviceAssemblies = new[] { typeof(IFileService).Assembly, typeof(AppDbContext).Assembly, typeof(IRepository<>).Assembly, typeof(IFileQueue).Assembly };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(_serviceAssemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAutoMapper(_callingAssembly, typeof(IFileService).Assembly);

            builder.RegisterMediatR(typeof(IFileService).Assembly);
            base.Load(builder);
        }
    }
}