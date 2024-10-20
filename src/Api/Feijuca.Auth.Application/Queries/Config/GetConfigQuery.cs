using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Models;
using MediatR;

namespace Feijuca.Auth.Application.Queries.Config
{
    public class GetConfigQuery : IRequest<Result<AuthSettings>>;
}
