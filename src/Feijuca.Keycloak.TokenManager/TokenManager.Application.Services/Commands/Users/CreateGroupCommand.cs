using MediatR;

using TokenManager.Application.Services.Requests.User;
using TokenManager.Common.Models;

namespace TokenManager.Application.Services.Commands.Users
{
    public record CreateGroupCommand(string Tenant, AddGroupRequest AddGroupRequest) : IRequest<Result>;
}
