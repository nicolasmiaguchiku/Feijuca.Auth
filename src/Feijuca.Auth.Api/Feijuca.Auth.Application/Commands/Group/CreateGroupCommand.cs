using Feijuca.Auth.Common.Models;

using Feijuca.Auth.Application.Requests.User;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Group
{
    public record CreateGroupCommand(string Tenant, AddGroupRequest AddGroupRequest) : IRequest<Result>;
}
