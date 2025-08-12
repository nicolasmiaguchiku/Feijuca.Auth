﻿using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;
using Feijuca.Auth.Http.Responses;
using Feijuca.Auth.Services;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.Users
{
    public class GetUsersQueryHandler(IUserRepository _userRepository, ITenantService _tenantService) : IRequestHandler<GetUsersQuery, Result<PagedResult<UserResponse>>>
    {
        public async Task<Result<PagedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var totalUsers = await _userRepository.GetTotalAsync(cancellationToken);
            var result = await _userRepository.GetAllAsync(request.GetUsersRequest.ToUserFilters(), totalUsers, cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<PagedResult<UserResponse>>.Failure(UserErrors.GetAllUsersError);
            }

            var filteredUsers = result.Data.AsEnumerable();

            if (request.GetUsersRequest.Usernames?.Any() ?? false)
            {
                filteredUsers = result.Data
                    .Where(x => request.GetUsersRequest.Usernames.Any(filter => x.Username.Contains(filter, StringComparison.OrdinalIgnoreCase)));
            }

            if (request.GetUsersRequest.AttributeKeys?.Count() == request.GetUsersRequest.AttributeValues?.Count())
            {
                for (int i = 0; i < request.GetUsersRequest.AttributeKeys?.Count(); i++)
                {
                    var key = request.GetUsersRequest.AttributeKeys?.ElementAt(i);
                    var value = request.GetUsersRequest.AttributeValues?.ElementAt(i);

                    filteredUsers = filteredUsers
                        .Where(u =>
                        {
                            u.Attributes.TryGetValue(key ?? "", out var values);
                            return values?.Any(v => v.Contains(value ?? "", StringComparison.OrdinalIgnoreCase)) ?? false;
                        });
                }
            }
            
            var users = filteredUsers.ToList();
            return Result<PagedResult<UserResponse>>.Success(users.ToUserResponse(request.GetUsersRequest.PageFilter, _tenantService.Tenant.Name, totalUsers));
        }
    }
}
