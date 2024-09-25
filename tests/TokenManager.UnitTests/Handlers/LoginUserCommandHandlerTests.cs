using AutoFixture;
using FluentAssertions;
using Moq;
using TokenManager.Application.Commands.Auth;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.UnitTests.Handlers
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

            DataHandle.Data.AccessToken
                .Should()
                .Be(tokenDetails.Access_Token);

            DataHandle.Data.ExpiresIn
                .Should()
                .Be(tokenDetails.Expires_In);

            DataHandle.Data.RefreshExpiresIn
                .Should()
                .Be(tokenDetails.Refresh_Expires_In);

            DataHandle.Data.RefreshToken
                .Should()
                .Be(tokenDetails.Refresh_Token);

            DataHandle.Data.TokenType
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
