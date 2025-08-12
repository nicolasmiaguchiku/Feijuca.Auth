using Feijuca.Auth.Application.Requests.Client;
using Mattioli.Configurations.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Client
{
    public record AddClientCommand(AddClientRequest AddClientRequest) : IRequest<Result<bool>>;
}
