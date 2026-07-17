using Feijuca.Auth.Application.Requests.UsersAttributes;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public record AddUserAttributeCommand(string UserName, AddUserAttributesRequest AddUserAttributesRequest) : ICommand<Result<bool>>;
}
