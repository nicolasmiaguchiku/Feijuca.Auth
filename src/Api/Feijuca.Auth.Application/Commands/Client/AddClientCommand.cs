using Feijuca.Auth.Application.Requests.Client;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Client
{
    public record AddClientCommand(AddClientRequest AddClientRequest) : ICommand<Result>;
}
