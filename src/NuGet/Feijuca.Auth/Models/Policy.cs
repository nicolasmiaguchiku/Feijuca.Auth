namespace Feijuca.Auth.Models;

public class Policy
{
    public required string Name { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}
