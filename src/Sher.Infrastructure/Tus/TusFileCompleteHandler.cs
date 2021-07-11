using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sher.Application.Files.CreateFile;
using tusdotnet.Models.Configuration;

namespace Sher.Infrastructure.Tus
{
    public static class TusFileCompleteHandler
    {
        public static Func<FileCompleteContext, Task> Handler => async context =>
        {
            var fileId = Guid.ParseExact(context.FileId, "N");
            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

            await mediator.Publish(new FileProcessedNotification(fileId));
        };
    }
}