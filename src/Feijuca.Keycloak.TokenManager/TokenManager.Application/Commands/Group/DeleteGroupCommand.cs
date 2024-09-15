using MediatR;

using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Group
{
    public record DeleteGroupCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
