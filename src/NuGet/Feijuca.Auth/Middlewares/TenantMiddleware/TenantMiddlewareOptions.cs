namespace Feijuca.Auth.Middlewares;

public sealed record TenantMiddlewareOptions
{
    public List<string> AvailableUrls { get; init; } = [];
}