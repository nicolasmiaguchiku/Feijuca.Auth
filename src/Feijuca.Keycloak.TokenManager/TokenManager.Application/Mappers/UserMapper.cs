using TokenManager.Application.Requests.Auth;
using TokenManager.Application.Requests.Pagination;
using TokenManager.Application.Requests.User;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Filters;

namespace TokenManager.Application.Mappers
{
    public static class UserMapper
    {
        public static User ToDomain(this AddUserRequest userRequest)
        {
            var atributtes = new Dictionary<string, string[]>();
            foreach (var item in userRequest.Attributes)
            {
                atributtes.Add(item.Key, item.Value);
            }

            return new User(userRequest.Username, userRequest.Password, userRequest.Email!, userRequest.FirstName!, userRequest.LastName!, atributtes);
        }

        public static IEnumerable<UserResponse> ToUsersResponse(this IEnumerable<User> users)
        {
            return users.Select(x => new UserResponse(x.Id, x.Username, x.Email, x.FirstName ?? "", x.LastName!, x.Attributes));
        }

        public static User ToLoginUserDomain(this LoginUserRequest loginUserRequest)
        {
            return new User(loginUserRequest.Username, loginUserRequest.Password);
        }

        public static Result<TokenDetailsResponse> ToTokenResponse(this Result<TokenDetails> tokenDetails)
        {
            var tokenDetailsResponse = new TokenDetailsResponse
            {
                AccessToken = tokenDetails.Data.Access_Token,
                ExpiresIn = tokenDetails.Data.Expires_In,
                RefreshToken = tokenDetails.Data.Refresh_Token,
                RefreshExpiresIn = tokenDetails.Data.Refresh_Expires_In,
                TokenType = tokenDetails.Data.Token_Type,
                Scopes = tokenDetails.Data.Scopes
            };

            return Result<TokenDetailsResponse>.Success(tokenDetailsResponse);
        }

        public static UserFilters ToUserFilters(this GetUsersRequest getUsersRequest)
        {
            var pageFilter = new PageFilter(getUsersRequest.PageFilter.Page, getUsersRequest.PageFilter.PageSize);
            return new UserFilters(pageFilter, getUsersRequest.Ids, getUsersRequest.Emails);
        }

        public static PagedResult<UserResponse> ToResponse(this IEnumerable<User> results, PageFilterRequest pageFilter, int totalResults)
        {
            return new PagedResult<UserResponse>
            {
                PageNumber = pageFilter.Page,
                PageSize = pageFilter.PageSize,
                Results = results.Select(x => x.ToResponse()),
                TotalResults = totalResults
            };
        }

        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse(user.Id, user.Username, user.Email, user.FirstName ?? "", user.LastName ?? "", user.Attributes);
        }
    }
}
