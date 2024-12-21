using System.Text.Json.Serialization;

namespace Feijuca.Auth.Models;

public class Realm
{
    [JsonIgnore]
    public FeijucaAuthClient? FeijucaAuthClient { get; set; }

    public bool DefaultSwaggerTokenGeneration { get; set; }

    public required string Name { get; set; }

    public required string Audience { get; set; }

    public required string Issuer { get; set; }

}
