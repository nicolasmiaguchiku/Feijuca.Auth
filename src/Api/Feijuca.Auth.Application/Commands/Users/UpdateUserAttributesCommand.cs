using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record UpdateUserAttributesCommand(Guid Id, Dictionary<string, string[]> Attributes) : IRequest<Result<bool>>;
}
