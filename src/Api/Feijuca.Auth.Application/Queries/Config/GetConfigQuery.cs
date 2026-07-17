using Feijuca.Auth.Common.Models;
using LiteBus.Queries.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Queries.Config
{
    public class GetConfigQuery : IQuery<Result<KeycloakSettings>>;
}
