using Common.Models;
using MediatR;

namespace Application.Commands.Users
{
    public record DeleteUserCommand(string Tenant, Guid Id) : IRequest<Result<bool>>;
}
