using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Models;
using MediatR;

namespace Feijuca.Auth.Application.Queries
{
    public class GetConfigCommand : IRequest<Result<AuthSettings>>;
}
