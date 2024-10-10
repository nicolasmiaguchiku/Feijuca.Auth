using Application.Requests.Role;
using Common.Models;
using MediatR;

namespace Application.Commands.Role
{
    public record AddRoleCommand(string Tenant, AddRoleRequest AddRoleRequest) : IRequest<Result<bool>>;
}
