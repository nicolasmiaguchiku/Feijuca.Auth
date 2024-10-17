using Feijuca.Auth.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Feijuca.Auth.Infra.CrossCutting.Middlewares
{
    public class ConfigValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IConfigRepository configRepository)
        {
            var configResult = configRepository.GetConfig();

            if (configResult == null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Configs not found! Please, use the endpoint to insert your Keycloak configs before start. Check the documentation: https://coderaw-io.github.io/Feijuca.Auth/docs/gettingStarted.html");
                return;
            }

            await _next(context);
        }
    }
}
