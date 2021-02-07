using Autofac;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(typeof(FileProcessingService), typeof(FilePersistenceService))
                .AsImplementedInterfaces();

            builder.RegisterType<FileProcessingQueue>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}