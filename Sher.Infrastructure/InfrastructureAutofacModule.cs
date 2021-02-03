using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Sher.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatR(typeof(Class1).Assembly);
            base.Load(builder);
        }
    }
}