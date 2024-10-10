using Application.Requests.User;
using Common.Models;
using MediatR;

namespace Application.Commands.Group
{
    public record CreateGroupCommand(string Tenant, AddGroupRequest AddGroupRequest) : IRequest<Result>;
}
