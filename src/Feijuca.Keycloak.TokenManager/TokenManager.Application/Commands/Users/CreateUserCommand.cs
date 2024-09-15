using MediatR;

using TokenManager.Application.Requests.User;
using TokenManager.Common.Models;

namespace TokenManager.Application.Commands.Users
{
    public record CreateUserCommand(string Tenant, AddUserRequest AddUserRequest) : IRequest<Common.Models.Result>;
}
