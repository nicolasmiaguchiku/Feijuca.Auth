using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record DeleteUserCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
