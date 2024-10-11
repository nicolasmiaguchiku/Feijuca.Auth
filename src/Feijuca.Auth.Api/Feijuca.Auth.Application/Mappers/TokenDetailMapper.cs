using Application.Responses;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TokenDetailMapper
    {
        public static TokenDetailsResponse ToTokenDetailResponse(this TokenDetails tokenDetails)
        {
            return new TokenDetailsResponse
            {
                AccessToken = tokenDetails.Access_Token,
                ExpiresIn = tokenDetails.Expires_In,
                Scope = tokenDetails.Scope,
                TokenType = tokenDetails.Token_Type,
                RefreshToken = tokenDetails.Refresh_Token,
                RefreshExpiresIn = tokenDetails.Refresh_Expires_In
            };
        }
    }
}
