using Feijuca.Auth.Application.Requests.User;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public record AddUserCommand(string Tenant, AddUserRequest AddUserRequest) : IRequest<Result<Guid>>;
}
