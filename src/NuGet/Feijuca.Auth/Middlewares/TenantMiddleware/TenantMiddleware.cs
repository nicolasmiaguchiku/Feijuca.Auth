using Feijuca.Auth.Services;
using Microsoft.AspNetCore.Http;

namespace Feijuca.Auth.Middlewares
{
    public class TenantMiddleware(RequestDelegate next, TenantMiddlewareOptions options)
    {
        private static readonly List<string> _defaultUrls = ["scalar", "openapi", "events", "favicon.ico"];
        private readonly List<string> _availableUrls = _defaultUrls
            .Union(options.AvailableUrls ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase)
            .ToList();


        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var path = context.Request.Path.Value!;
            if (_availableUrls.Exists(path.Contains))
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
