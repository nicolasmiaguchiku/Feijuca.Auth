using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public record DeleteUserAttributesCommand(string Username, IEnumerable<string> DeleteUserAttributeRequest) : ICommand<Result<bool>>;
}
