using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Http.Responses;
namespace Feijuca.Auth.Application.Mappers
{
    public static class TokenDetailMapper
    {
        public static TokenDetailsResponse ToTokenDetailResponse(this TokenDetails tokenDetails)
        {
            return new TokenDetailsResponse(tokenDetails.Access_Token,
                tokenDetails.Expires_In,
                tokenDetails.Refresh_Expires_In,
                tokenDetails.Refresh_Token,
                tokenDetails.Token_Type,
                tokenDetails.Scope);
        }
    }
}
