﻿using Mattioli.Configurations.Models;
using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class AddUserCommandHandler(IUserRepository userRepository) : IRequestHandler<AddUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.AddUserRequest.ToDomain(request.Tenant);
            var result = await _userRepository.CreateAsync(request.Tenant, user, cancellationToken);

            if (result.IsSuccess)
            {
                var keycloakUser = await _userRepository.GetAsync(request.Tenant, user.Username, cancellationToken);
                result = await _userRepository.ResetPasswordAsync(keycloakUser.Data.Id, request.Tenant, user.Password, cancellationToken);

                if (result.IsSuccess)
                {
                    return Result<Guid>.Success(keycloakUser.Data.Id);
                }
            }

            return Result<Guid>.Failure(result.Error);
        }
    }
}
