using System;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Sher.Application.Files.CreateFile;
using tusdotnet.Models.Configuration;

namespace Sher.Infrastructure.Tus
{
    public static class TusCreateCompleteHandler
    {
        public static Func<CreateCompleteContext, Task> Handler => async context =>
        {
            var userId = Guid.Parse(context.HttpContext.GetUserId());

            var fileId = Guid.ParseExact(context.FileId, "N");

            var fileName = context.Metadata["fileName"].GetString(Encoding.UTF8);

            Guid? parentDirectoryId = null;
            if (context.Metadata.ContainsKey("parentDirectoryId"))
            {
                parentDirectoryId = Guid.Parse(context.Metadata["parentDirectoryId"].GetString(Encoding.UTF8));
            }

            var contentType = GetContentTypeFromFileName(fileName);

            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

            await mediator.Send(new CreateFileCommand(
                fileId,
                parentDirectoryId,
                userId,
                fileName,
                contentType,
                context.UploadLength));
        };

        private static string GetContentTypeFromFileName(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}