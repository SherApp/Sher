using System;
using System.Net;
using System.Threading.Tasks;
using tusdotnet.Models.Configuration;

namespace Sher.Infrastructure.Tus
{
    public static class TusBeforeCreateHandler
    {
        public static Func<BeforeCreateContext, Task> Handler => context =>
        {
            if (!context.Metadata.TryGetValue("fileName", out var fileName) || fileName.HasEmptyValue)
            {
                context.FailRequest(HttpStatusCode.BadRequest, "Invalid fileName");
            }

            if (context.Metadata.ContainsKey("parentDirectoryId") && !context.Metadata.TryGetGuidValue("parentDirectoryId", out _))
            {
                context.FailRequest(HttpStatusCode.BadRequest, "Invalid parentDirectoryId");
            }

            return Task.CompletedTask;
        };
    }
}