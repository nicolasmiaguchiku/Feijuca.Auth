using Application.Requests.User;
using MediatR;

namespace Application.Commands.Users
{
    public record CreateUserCommand(string Tenant, AddUserRequest AddUserRequest) : IRequest<Common.Models.Result>;
}
