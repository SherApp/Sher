using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sher.Infrastructure
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext) =>
            httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}