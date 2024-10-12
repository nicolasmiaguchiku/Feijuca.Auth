using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record DeleteGroupCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
