using Feijuca.Auth.Application.Requests.User;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public record CreateUserCommand(AddUserRequest AddUserRequest) : IRequest<Common.Models.Result>;
}
