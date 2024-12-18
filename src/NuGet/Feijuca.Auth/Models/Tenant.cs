namespace Feijuca.Auth.Models;

public class Tenant
{
    public required string Name { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
}
