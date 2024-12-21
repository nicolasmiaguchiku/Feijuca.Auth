using System.Text.Json.Serialization;

namespace Feijuca.Auth.Models;

public class Realm
{
    [JsonIgnore]
    public FeijucaAuthClient? FeijucaAuthClient { get; init; }
    public required string Name { get; init; }
    public required string Audience { get; init; }
    public required string Issuer { get; init; }
}
