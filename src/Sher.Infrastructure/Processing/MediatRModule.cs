using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Sher.Application.Processing;
using Module = Autofac.Module;

namespace Sher.Infrastructure.Processing
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

            builder.RegisterGenericDecorator(typeof(DomainEventHandlerLoggingDecorator<>), typeof(INotificationHandler<>));

            builder.RegisterGenericDecorator(typeof(CommandHandlerUnitOfWorkDecorator<,>), typeof(IRequestHandler<,>));

            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerDependency();

            base.Load(builder);
        }
    }
}