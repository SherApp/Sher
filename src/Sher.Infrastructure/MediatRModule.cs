using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Application.CommandProcessing;
using Module = Autofac.Module;

namespace Sher.Infrastructure
{
    public class MediatRModule : Module
    {
        private static Assembly ApplicationAssembly => typeof(ValidationPipelineBehavior<,>).Assembly;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ApplicationAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .SingleInstance();
 
            builder.RegisterMediatR(ApplicationAssembly);

            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            base.Load(builder);
        }
    }
}