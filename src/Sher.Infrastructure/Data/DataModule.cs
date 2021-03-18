using Autofac;
using Sher.Core.Base;

namespace Sher.Infrastructure.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerDependency();
        }
    }
}