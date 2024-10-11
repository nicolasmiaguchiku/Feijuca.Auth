using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infra.CrossCutting.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var tenantId = context.Request.RouteValues["tenant"]?.ToString();

            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantService.SetTenant(tenantId);
            }

            await _next(context);
        }
    }
}
