using Feijuca.Auth.Application.Requests.UsersAttributes;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public record DeleteUserAttributesCommand(string Username, IEnumerable<string> DeleteUserAttributeRequest) : IRequest<Result<bool>>;
}
