using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Config
{
    public record UpdateFeijucaConfigWithClientIdAndSecretCommandHandler : ICommand<Result<bool>>;
}
