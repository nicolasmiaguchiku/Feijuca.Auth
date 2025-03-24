using Feijuca.Auth.Application.Requests.User;
using Feijuca.Auth.Common.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record UpdateUserCommand(Guid Id, AddUserRequest UserRequest) : IRequest<Result<bool>>;
}
