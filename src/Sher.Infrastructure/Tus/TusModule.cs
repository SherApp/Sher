using Autofac;
using Sher.Application.Files;

namespace Sher.Infrastructure.Tus
{
    public class TusModule : Module
    {
        private readonly string _baseDiskStorePath;

        public TusModule(string baseDiskStorePath)
        {
            _baseDiskStorePath = baseDiskStorePath;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TusDiskStorePathProvider>()
                .WithParameter("basePath", _baseDiskStorePath)
                .AsSelf()
                .As<IUploaderFileStorePathProvider>();
        }
    }
}