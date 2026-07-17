using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;
using Feijuca.Auth.Application.Requests.User;

namespace Feijuca.Auth.Application.Commands.User
{
    public record AddUserCommand(string Tenant, AddUserRequest AddUserRequest) : ICommand<Result<Guid>>;
}
