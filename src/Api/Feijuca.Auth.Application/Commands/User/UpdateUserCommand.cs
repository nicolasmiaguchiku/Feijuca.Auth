using Feijuca.Auth.Application.Requests.User;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record UpdateUserCommand(Guid Id, AddUserRequest UserRequest) : IRequest<Result<bool>>;
}
