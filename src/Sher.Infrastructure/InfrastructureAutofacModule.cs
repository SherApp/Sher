using System.Reflection;
using Autofac;
using Sher.Infrastructure.Data;
using Module = Autofac.Module;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Sher.Application.Access;
using Sher.Application.Files;
using Sher.Core.Files;
using Sher.Infrastructure.Processing;
using Sher.Infrastructure.Tus;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        private readonly Assembly _callingAssembly;
        private readonly Assembly[] _serviceAssemblies;
        private readonly IConfiguration _configuration;

        public InfrastructureAutofacModule(IConfiguration configuration, Assembly callingAssembly = null)
        {
            _configuration = configuration;
            _callingAssembly = callingAssembly;
            _serviceAssemblies = new[]
            {
                typeof(IUploaderFileStorePathProvider).Assembly,
                typeof(AppDbContext).Assembly,
                typeof(IFileRepository).Assembly
            };
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_serviceAssemblies)
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType<JwtIssuer>();

            builder.RegisterAutoMapper(_callingAssembly, typeof(IUploaderFileStorePathProvider).Assembly);

            builder.RegisterModule<MediatRModule>();
            builder.RegisterModule(new DataModule(_configuration.GetConnectionString("Default")));
            builder.RegisterModule(new TusModule(_configuration["Tus:DiskStorePath"]));

            base.Load(builder);
        }
    }
}