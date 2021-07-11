using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sher.Application.Files.CreateFile;
using tusdotnet.Models.Configuration;

namespace Sher.Infrastructure.Tus
{
    public static class TusBeforeCreateHandler
    {
        public static Func<BeforeCreateContext, Task> Handler => async context =>
        {
            var userId = Guid.Parse(context.HttpContext.GetUserId());

            if (!Guid.TryParseExact(context.FileId, "N", out var fileId))
            {
                context.FailRequest(HttpStatusCode.BadRequest, "Invalid fileId");
            }

            if (!context.Metadata.TryGetValue("fileName", out var fileName) || fileName.HasEmptyValue)
            {
                context.FailRequest(HttpStatusCode.BadRequest, "Invalid fileName");
            }

            if (!context.Metadata.TryGetGuidValue("parentDirectoryId", out var parentDirectoryId))
            {
                context.FailRequest(HttpStatusCode.BadRequest, "Invalid parentDirectoryId");
            }

            if (context.HasFailed)
            {
                return;
            }

            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

            await mediator.Send(new CreateFileCommand(
                fileId,
                parentDirectoryId,
                userId,
                fileName!.GetString(Encoding.UTF8),
                context.UploadLength));
        };
    }
}