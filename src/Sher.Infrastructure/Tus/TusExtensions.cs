using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

namespace Sher.Infrastructure.Tus
{
    public static class TusExtensions
    {
        public static DefaultTusConfiguration SetupTus(this HttpContext httpContext)
        {
            var storePathProvider = httpContext.RequestServices.GetRequiredService<TusDiskStorePathProvider>();
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var storePath = storePathProvider.GetPathForUserOfId(userId).Result;

            return new DefaultTusConfiguration
            {
                UrlPath = "/api/file",
                Store = new TusDiskStore(storePath),
                Events = new Events
                {
                    OnAuthorizeAsync = TusAuthorizationHandler.Handler,
                    OnBeforeCreateAsync = TusBeforeCreateHandler.Handler,
                    OnCreateCompleteAsync = TusCreateCompleteHandler.Handler,
                    OnFileCompleteAsync = TusFileCompleteHandler.Handler
                }
            };
        }

        internal static bool TryGetGuidValue(
            this IReadOnlyDictionary<string, Metadata> metadata,
            string key,
            out Guid value)
        {
            if (metadata.TryGetValue(key, out var fileMeta) &&
                Guid.TryParse(fileMeta.GetString(Encoding.UTF8), out value)) return true;

            value = default;
            return false;
        }
    }
}