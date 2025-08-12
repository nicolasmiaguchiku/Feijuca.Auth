﻿using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Services;

using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public class AddUserAttributeCommandHandler(IUserRepository userRepository, ITenantService tenantService) : IRequestHandler<AddUserAttributeCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddUserAttributeCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(tenantService.Tenant.Name, request.UserName, cancellationToken);
            if (user.IsSuccess)
            {
                var userNewAttributes = user.Data.Attributes;
                foreach (var item in request.AddUserAttributesRequest.Attributes)
                {
                    userNewAttributes.TryAdd(item.Key, item.Value);
                }

                user.Data.Attributes = userNewAttributes;

                var result = await userRepository.UpdateUserAsync(user.Data.Id, user.Data, cancellationToken);

                if (result.IsSuccess)
                {
                    return Result<bool>.Success(true);
                }

                return Result<bool>.Failure(UserErrors.ErrorWhileAddedUserAttribute);
            }

            return Result<bool>.Failure(UserErrors.GetUserError);
        }
    }
}
