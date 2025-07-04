using Feijuca.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Feijuca.Auth.Middlewares
{
    public class TenantMiddleware(RequestDelegate next)
    {

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
        {
            var endpoint = context.GetEndpoint();
            var hasAuthorize = endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null;

            if (!hasAuthorize)
            {
                await next(context);
                return;
            }

            var tenants = tenantService.GetTenants();
            var user = tenantService.GetUser();

            if (!tenants.Any() || user.Id == Guid.Empty)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new { error = "Jwt token authorization header is required." };

                await context.Response.WriteAsJsonAsync(response);

                return;
            }

            tenantService.SetTenants(tenants);
            tenantService.SetUser(user);

            await next(context);
        }
    }
}
