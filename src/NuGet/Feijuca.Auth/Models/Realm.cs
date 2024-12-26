using System.Text.Json.Serialization;

namespace Feijuca.Auth.Models;

public class Realm
{
    public bool DefaultSwaggerTokenGeneration { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public string? Audience { get; set; }

    [JsonIgnore]
    public string? Issuer { get; set; }

}
