namespace Feijuca.Auth.Middlewares.TenantMiddleware;

public sealed record TenantMiddlewareOptions
{
    public List<string> AvailableUrls { get; init; } = [];
}