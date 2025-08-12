using Mattioli.Configurations.Models;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Config
{
    public record UpdateFeijucaConfigWithClientIdAndSecretCommandHandler : IRequest<Result<bool>>;
}
