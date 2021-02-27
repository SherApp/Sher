using System.Reflection;
using Autofac;
using Sher.Infrastructure.Data;
using Sher.Infrastructure.Data.Repositories;
using Module = Autofac.Module;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Sher.Application.Files;
using Sher.Core.Base;
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
            _serviceAssemblies = new[] { typeof(FileProcessingContext).Assembly, typeof(AppDbContext).Assembly, typeof(IRepository<>).Assembly, typeof(IFileQueue).Assembly };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(_serviceAssemblies)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAutoMapper(_callingAssembly, typeof(FileProcessingContext).Assembly);

            builder.RegisterModule<MediatRModule>();

            base.Load(builder);
        }
    }
}