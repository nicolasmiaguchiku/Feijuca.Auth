namespace Feijuca.Auth.Models;

public class Realm
{
    public required string FeijucaAuthClientId { get; init; }
    public required string FeijucaAuthClientSecret { get; init; }
    public required string Name { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
    public bool DefaultSwaggerTokenGeneration { get; init; }
}
