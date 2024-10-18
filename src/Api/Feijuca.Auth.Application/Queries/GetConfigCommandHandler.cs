using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Models;
using MediatR;

namespace Feijuca.Auth.Application.Queries
{
    public class GetConfigCommandHandler(IConfigRepository configRepository) : IRequestHandler<GetConfigCommand, Result<AuthSettings>>
    {
        private readonly IConfigRepository _configRepository = configRepository;

        public Task<Result<AuthSettings>> Handle(GetConfigCommand request, CancellationToken cancellationToken)
        {
            var result = _configRepository.GetConfig();

            if (result is null)
            {
                return Task.FromResult(Result<AuthSettings>.Failure(ConfigErrors.NoConfigInserted));
            }

            return Task.FromResult(Result<AuthSettings>.Success(result));
        }
    }
}
