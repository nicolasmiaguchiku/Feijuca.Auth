using Feijuca.Auth.Services;
using Microsoft.AspNetCore.Http;

namespace Feijuca.Auth.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var path = context.Request.Path.Value!;
            var availableStartingUrls = new List<string> { "scalar", "openapi", "events", "favicon.ico" };
            if (availableStartingUrls.Exists(path.Contains))
            {
                await next(context);
                return;
            }

            var tenant = tenantService.GetTenantFromToken();
            var user = tenantService.GetUserFromToken();

            if (string.IsNullOrEmpty(tenant.Name) || user.Id == Guid.Empty)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = new { error = "Jwt token authorization header is required." };
                await context.Response.WriteAsJsonAsync(response);
                return;
            }

            tenantService.SetTenant(tenant);
            tenantService.SetUser(user);

            await next(context);
        }
    }
}
