using AutoFixture;

using Feijuca.Auth.Application.Commands.Users;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;

using FluentAssertions;

using Moq;

namespace Feijuca.Auth.Api.UnitTests.Handlers
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Fixture _autoFixture = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly CreateUserCommandHandler _createUserCommandHandler;

        public CreateUserCommandHandlerTests()
        {
            _createUserCommandHandler = new(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleWhenInformAValidUser_ShouldCreateANewUserAndReturnsTrue()
        {
            // Arrange
            var createUserCommand = _autoFixture.Create<CreateUserCommand>();
            var user = _autoFixture.Create<User>();

            var errorMessage = _autoFixture.Create<string>();
            UserErrors.SetTechnicalMessage(errorMessage);

            _userRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<bool>.Success(true));

            _userRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(Result<User>.Success(user));

            _userRepositoryMock
                .Setup(x => x.ResetPasswordAsync(It.IsAny<Guid>(), It.IsAny<string>()))
                .ReturnsAsync(Result<bool>.Success(true));

            //Act
            var resultHandle = await _createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

            //Assert
            resultHandle.IsSuccess
                .Should()
                .Be(true);

            _userRepositoryMock
                .Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);

            _userRepositoryMock
                .Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);

            _userRepositoryMock
                .Verify(x => x.ResetPasswordAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);

            _userRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task HandleWhenInformAInvalidConfig_ShouldNotCreateANewUserAndReturnsFalse()
        {
            // Arrange
            var createUserCommand = _autoFixture.Create<CreateUserCommand>();

            _userRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(Result<bool>.Failure(UserErrors.UserCreationError));

            //Act
            var resultHandle = await _createUserCommandHandler.Handle(createUserCommand, CancellationToken.None);

            //Assert
            resultHandle.IsSuccess
                .Should()
                .Be(false);

            resultHandle.Error.Description
                .Should()
                .Be(UserErrors.UserCreationError.Description);

            _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Never);
            _userRepositoryMock.Verify(x => x.ResetPasswordAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        }
    }
}
