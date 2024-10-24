using Feijuca.Auth.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Feijuca.Auth.Infra.CrossCutting.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var tenantId = context.Request.Headers["Tenant"].ToString();

            if (string.IsNullOrEmpty(tenantId))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = new { error = "Tenant header is required." };
                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            tenantService.SetTenant(tenantId);
            await next(context);
        }
    }
}