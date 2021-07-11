using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

namespace Sher.Infrastructure.Tus
{
    public static class TusExtensions
    {
        private static string DefaultTusDiskStorePath = "wwwroot/u/";

        public static DefaultTusConfiguration SetupTus(this HttpContext httpContext, string storePath = null)
        {
            storePath ??= DefaultTusDiskStorePath;

            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is not null)
            {
                storePath = Path.Combine(storePath, userId);
            }

            return new DefaultTusConfiguration
            {
                UrlPath = "/api/file",
                Store = new TusDiskStore(storePath),
                Events = new Events
                {
                    OnAuthorizeAsync = TusAuthorizationHandler.Handler,
                    OnBeforeCreateAsync = TusBeforeCreateHandler.Handler,
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