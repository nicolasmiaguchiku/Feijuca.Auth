using Application.Commands.Auth;
using AutoFixture;
using Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Handlers
{
    public class RefreshTokenCommandHandlerTests
    {
        private readonly Fixture _autoFixture = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly RefreshTokenCommandHandler _refreshTokenCommandHandler;

        public RefreshTokenCommandHandlerTests()
        {
            _refreshTokenCommandHandler = new(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleWhenInformAValidToken_ShouldBeRefreshedTheToken()
        {
            // Arrange
            var refreshTokenCommand = _autoFixture.Create<RefreshTokenCommand>();

            var tokenDetails = _autoFixture.Create<TokenDetails>();
            var successResult = Result<TokenDetails>.Success(tokenDetails);

            _userRepositoryMock
                .Setup(x => x.RefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(successResult);

            //Act
            var resultHandle = await _refreshTokenCommandHandler.Handle(refreshTokenCommand, CancellationToken.None);

            //Assert
            resultHandle.IsSuccess
                .Should()
                .Be(true);

            resultHandle.Response.AccessToken
                .Should()
                .Be(tokenDetails.Access_Token);

            resultHandle.Response.ExpiresIn
                .Should()
                .Be(tokenDetails.Expires_In);

            resultHandle.Response.RefreshExpiresIn
                .Should()
                .Be(tokenDetails.Refresh_Expires_In);

            resultHandle.Response.RefreshToken
                .Should()
                .Be(tokenDetails.Refresh_Token);

            resultHandle.Response.TokenType
                .Should()
                .Be(tokenDetails.Token_Type);
        }

        [Fact]
        public async Task HandleWhenInformAInvalidUser_ShouldNotBeLoggedAndShouldReturnsAnError()
        {
            // Arrange
            var refreshTokenCommand = _autoFixture.Create<RefreshTokenCommand>();

            var error = _autoFixture.Create<Error>();
            var failureResult = Result<TokenDetails>.Failure(error);

            _userRepositoryMock
                .Setup(x => x.RefreshTokenAsync(It.IsAny<string>()))
                .ReturnsAsync(failureResult);

            //Act
            var resultHandle = await _refreshTokenCommandHandler.Handle(refreshTokenCommand, CancellationToken.None);

            //Assert
            resultHandle.IsSuccess
                .Should()
                .Be(false);

            resultHandle.Error.Description
                .Should()
                .Contain(error.Description);

            resultHandle.Error.Code
                .Should()
                .Contain(error.Code);
        }
    }
}
