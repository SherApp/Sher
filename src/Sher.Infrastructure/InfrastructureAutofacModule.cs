using System.Reflection;
using Autofac;
using Sher.Infrastructure.Data;
using Module = Autofac.Module;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Sher.Application.Access;
using Sher.Application.Files;
using Sher.Core.Files;
using Sher.Infrastructure.FileProcessing.Interfaces;
using Sher.Infrastructure.Processing;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        private readonly Assembly _callingAssembly;
        private readonly Assembly[] _serviceAssemblies;
        private readonly string _connectionString;

        public InfrastructureAutofacModule(string connectionString, Assembly callingAssembly = null)
        {
            _connectionString = connectionString;
            _callingAssembly = callingAssembly;
            _serviceAssemblies = new[]
            {
                typeof(FileProcessingContext).Assembly,
                typeof(AppDbContext).Assembly,
                typeof(IFileRepository).Assembly,
                typeof(IFileQueue).Assembly
            };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_serviceAssemblies)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType<JwtIssuer>();

            builder.RegisterAutoMapper(_callingAssembly, typeof(FileProcessingContext).Assembly);

            builder.RegisterModule<MediatRModule>();
            builder.RegisterModule(new DataModule(_connectionString));

            base.Load(builder);
        }
    }
}