using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Requests.Auth;
using Feijuca.Auth.Application.Requests.GroupUsers;
using Feijuca.Auth.Application.Requests.Pagination;
using Feijuca.Auth.Application.Requests.User;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;

namespace Feijuca.Auth.Application.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(this AddUserRequest userRequest, string tenant)
        {
            var atributtes = new Dictionary<string, string[]>
            {
                { "tenant", [tenant] }
            };

            foreach (var item in userRequest.Attributes?.Where(x => x.Key != "tenant") ?? [])
            {
                atributtes.Add(item.Key, item.Value);
            }

            return new User(userRequest.Username, userRequest.Password, userRequest.Email!, userRequest.FirstName!, userRequest.LastName!, atributtes);
        }

        public static IEnumerable<UserResponse> ToUsersResponse(this IEnumerable<User> users, string tenant)
        {
            return users.Select(x => new UserResponse(x.Id, x.Username, x.Email, x.FirstName ?? "", x.LastName!, tenant, x.Attributes));
        }

        public static User ToLoginUserDomain(this LoginUserRequest loginUserRequest)
        {
            return new User(loginUserRequest.Username, loginUserRequest.Password);
        }

        public static Result<TokenDetailsResponse> ToTokenResponse(this Result<TokenDetails> tokenDetails)
        {
            var tokenDetailsResponse = new TokenDetailsResponse
            {
                AccessToken = tokenDetails.Response.Access_Token,
                ExpiresIn = tokenDetails.Response.Expires_In,
                RefreshToken = tokenDetails.Response.Refresh_Token,
                RefreshExpiresIn = tokenDetails.Response.Refresh_Expires_In,
                TokenType = tokenDetails.Response.Token_Type,
                Scope = tokenDetails.Response.Scope
            };

            return Result<TokenDetailsResponse>.Success(tokenDetailsResponse);
        }

        public static UserFilters ToUserFilters(this GetUsersRequest getUsersRequest)
        {
            var pageFilter = new PageFilter(getUsersRequest.PageFilter.Page, getUsersRequest.PageFilter.PageSize);
            return new UserFilters(pageFilter, getUsersRequest.Ids, getUsersRequest.Usernames);
        }

        public static UserFilters ToUserFilters(this GetUsersGroupRequest getUsersRequest)
        {
            var pageFilter = new PageFilter(getUsersRequest.PageFilter.Page, getUsersRequest.PageFilter.PageSize);
            return new UserFilters(pageFilter, [], getUsersRequest.Emails);
        }

        public static PagedResult<UserResponse> ToUserResponse(this IEnumerable<User> results, PageFilterRequest pageFilter, string tenant, int totalResults)
        {
            return new PagedResult<UserResponse>
            {
                PageNumber = pageFilter.Page,
                PageSize = pageFilter.PageSize,
                Results = results.Select(x => x.ToResponse(tenant)),
                TotalResults = totalResults,
            };
        }

        public static UserResponse ToResponse(this User user, string tenant)
        {
            return new UserResponse(user.Id,
                user.Username,
                user.Email,
                user.FirstName ?? "",
                user.LastName ?? "",
                tenant,
                user.Attributes);
        }
    }
}
