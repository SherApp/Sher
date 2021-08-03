using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Sher.Api.Common;
using Sher.Core.Base;

namespace Sher.Api.Utils
{
    public static class GlobalExceptionHandler
    {
        public static RequestDelegate Delegate => async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            ErrorModel model;
            var error = exceptionHandlerPathFeature?.Error;

            if (error is BusinessRuleViolationException)
            {
                model = new ErrorModel(error.Message);
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            }
            else
            {
                model = new ErrorModel("Internal server error");
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(model, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        };
    }
}