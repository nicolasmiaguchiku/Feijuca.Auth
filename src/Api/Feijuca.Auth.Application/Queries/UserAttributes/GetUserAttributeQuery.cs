using LiteBus.Queries.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Queries.UserAttributes
{
    public record GetUserAttributeQuery(string Username) : IQuery<Result<Dictionary<string, string[]>>>;
}
