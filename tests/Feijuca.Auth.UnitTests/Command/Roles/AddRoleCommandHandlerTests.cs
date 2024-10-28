using AutoFixture;
using Feijuca.Auth.Application.Commands.Role;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Feijuca.Auth.Api.UnitTests.Command.Roles
{
    public class AddRoleCommandHandlerTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly Mock<IRoleRepository> _roleRepositoryMock = new();
        private readonly AddRoleCommandHandler _handler;

        public AddRoleCommandHandlerTests()
        {
            _handler = new AddRoleCommandHandler(_roleRepositoryMock.Object);
        }

        [Fact]
        public async Task Given_ValidRoleData_When_AddRoleIsCalled_Then_ShouldAddRoleSuccessfully()
        {
            var addRoleCommand = _fixture.Create<AddRoleCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var addRoleResult = Result<bool>.Success(true);

            _roleRepositoryMock
                .Setup(repo => repo.AddRoleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addRoleResult);

            // Act
            var result = await _handler.Handle(addRoleCommand, cancellationToken);

            // Assert
            result
                .IsSuccess
                .Should()
                .BeTrue();

            _roleRepositoryMock.Verify(repo => repo.AddRoleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            _roleRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Given_InvalidRoleData_When_AddRoleIsCalled_Then_ShouldAddRoleFailure()
        {
            var addRoleCommand = _fixture.Create<AddRoleCommand>();
            var cancellationToken = _fixture.Create<CancellationToken>();
            var addRoleResult = Result<bool>.Failure(RoleErrors.AddRoleErrors);

            _roleRepositoryMock
                .Setup(repo => repo.AddRoleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addRoleResult);

            // Act
            var result = await _handler.Handle(addRoleCommand, cancellationToken);

            // Assert
            result
                .Error
                .Should()
                .Be(RoleErrors.AddRoleErrors);

            _roleRepositoryMock.Verify(repo => repo.AddRoleAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            _roleRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
