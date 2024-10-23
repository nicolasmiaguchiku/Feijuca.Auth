using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Config
{
    public class AddConfigCommandHandler(IConfigRepository configRepository) : IRequestHandler<AddConfigCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddConfigCommand request, CancellationToken cancellationToken)
        {
            var result = await configRepository.AddConfigAsync(request.Request.ToEntity(), cancellationToken);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ConfigErrors.InsertConfig);
        }
    }
}
