using Autofac;

namespace Sher.Infrastructure.FileProcessing
{
    public class FileProcessingModule<TContext> : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterTypes(typeof(FileProcessingService<TContext>), typeof(FilePersistenceService))
                .AsImplementedInterfaces();

            builder.RegisterType<FileProcessingQueue<TContext>>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}