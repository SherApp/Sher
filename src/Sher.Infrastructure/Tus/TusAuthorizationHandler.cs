using System;
using System.Net;
using System.Threading.Tasks;
using tusdotnet.Models.Configuration;

namespace Sher.Infrastructure.Tus
{
    public static class TusAuthorizationHandler
    {
        public static Func<AuthorizeContext, Task> Handler => context =>
        {
            if (context.HttpContext.User.Identity is null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.FailRequest(HttpStatusCode.Unauthorized);
            }
            
            return Task.CompletedTask;
        };
    }
}