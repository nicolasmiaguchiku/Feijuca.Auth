﻿using Feijuca.Auth.Application.Mappers;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Http.Responses;
using Feijuca.Auth.Models;
using Feijuca.Auth.Services;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class LoginCommandHandler(IUserRepository userRepository, ITenantService tenantService) : IRequestHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        public async Task<Result<TokenDetailsResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            tenantService.SetTenants([new Tenant(request.Tenant)]);

            var result = await userRepository.LoginAsync(request.LoginUser.Username, request.LoginUser.Password, cancellationToken);

            return result.IsSuccess ? Result<TokenDetailsResponse>.Success(result.Data.ToTokenDetailResponse()) : Result<TokenDetailsResponse>.Failure(result.Error);
        }
    }
}
