using MediatR;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Users
{
    public record DeleteUserCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
