using System.Text.Json.Serialization;

namespace Feijuca.Auth.Models;

public class Realm
{
    public bool DefaultSwaggerTokenGeneration { get; set; }

    public required string Name { get; set; }

    [JsonIgnore]
    public required string Audience { get; set; }

    [JsonIgnore]
    public required string Issuer { get; set; }

}
