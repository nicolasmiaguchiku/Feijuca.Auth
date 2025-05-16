using Feijuca.Auth.Middlewares;
using Feijuca.Auth.Models;

using Microsoft.AspNetCore.Builder;

namespace Feijuca.Auth.Extensions;

public static class TenantMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder app, Action<TenantMiddlewareOptions> configure)
    {
        var options = new TenantMiddlewareOptions();
        configure(options);

        return app.UseMiddleware<TenantMiddleware>(options);
    }
}