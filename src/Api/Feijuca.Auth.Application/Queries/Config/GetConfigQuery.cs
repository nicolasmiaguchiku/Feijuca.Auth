using Feijuca.Auth.Common.Models;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Queries.Config
{
    public class GetConfigQuery : IRequest<Result<KeycloakSettings>>;
}
