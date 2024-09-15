using MediatR;
using TokenManager.Application.Requests.Role;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Role
{
    public record AddRoleCommand(string Tenant, AddRoleRequest AddRoleRequest) : IRequest<Result<bool>>;
}
