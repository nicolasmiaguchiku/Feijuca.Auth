using MediatR;

namespace Feijuca.Auth.Application.Queries.Realm
{
    public record GetRealmConfigurationQuery(string Name) : IRequest<string>;
}
