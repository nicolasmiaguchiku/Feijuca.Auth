using Application.Commands.Auth;
using AutoFixture;
using Common.Models;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Handlers
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Fixture _autoFixture = new();
        private readonly Mock<IAuthRepository> _authRepositoryMock = new();
        private readonly LoginCommandHandler _loginUserCommandHandler;

        public LoginUserCommandHandlerTests()
        {
            _loginUserCommandHandler = new(_authRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleWhenInformAValidUser_ShouldLoggedAndGenerateNewTokenDetails()
        {
            // Arrange
            var loginUserCommand = _autoFixture.Create<LoginCommand>();

            var tokenDetails = _autoFixture.Create<TokenDetails>();
            var successData = Result<TokenDetails>.Success(tokenDetails);

            _authRepositoryMock
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<User>()))
                .ReturnsAsync(successData);

            //Act
            var DataHandle = await _loginUserCommandHandler.Handle(loginUserCommand, CancellationToken.None);

            //Assert
            DataHandle.IsSuccess
                .Should()
                .Be(true);

            DataHandle.Response.AccessToken
                .Should()
                .Be(tokenDetails.Access_Token);

            DataHandle.Response.ExpiresIn
                .Should()
                .Be(tokenDetails.Expires_In);

            DataHandle.Response.RefreshExpiresIn
                .Should()
                .Be(tokenDetails.Refresh_Expires_In);

            DataHandle.Response.RefreshToken
                .Should()
                .Be(tokenDetails.Refresh_Token);

            DataHandle.Response.TokenType
                .Should()
                .Be(tokenDetails.Token_Type);
        }

        [Fact]
        public async Task HandleWhenInformAInvalidUser_ShouldNotBeLoggedAndShouldReturnsAnError()
        {
            // Arrange
            var loginUserCommand = _autoFixture.Create<LoginCommand>();

            var error = _autoFixture.Create<Error>();
            var failureData = Result<TokenDetails>.Failure(error);

            _authRepositoryMock
                .Setup(x => x.LoginAsync(It.IsAny<string>(), It.IsAny<User>()))
                .ReturnsAsync(failureData);

            //Act
            var DataHandle = await _loginUserCommandHandler.Handle(loginUserCommand, CancellationToken.None);

            //Assert
            DataHandle.IsSuccess
                .Should()
                .Be(false);

            DataHandle.Error.Description
                .Should()
                .Contain(error.Description);

            DataHandle.Error.Code
                .Should()
                .Contain(error.Code);
        }
    }
}
