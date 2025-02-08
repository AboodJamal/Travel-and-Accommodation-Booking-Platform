using Application.Commands.UserCommands;
using Application.Handlers.UserHandlers;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Interfaces;
using TABP.Hashing.PasswordUtils;
using Moq;
using TABP.Domain.Entities;


namespace TABP.Tests.UserTests.UserCommands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<UserRepositoryInterface> _mockUserRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<PasswordHandlerInterface> _mockPasswordHandler;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUserRepository = new Mock<UserRepositoryInterface>();
            _mockMapper = new Mock<IMapper>();
            _mockPasswordHandler = new Mock<PasswordHandlerInterface>();
            _handler = new CreateUserCommandHandler(_mockUserRepository.Object, _mockMapper.Object, _mockPasswordHandler.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesUser()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PhoneNumber = "1234567890"
            };

            var user = new User { Id = Guid.NewGuid() };
            var salt = new byte[] { 1, 2, 3 };
            var hashedPassword = "hashedPassword";

            _mockMapper
                .Setup(mapper => mapper.Map<User>(command))
                .Returns(user);

            _mockPasswordHandler
                .Setup(handler => handler.GenerateHashingSaltValue())
                .Returns(salt);

            _mockPasswordHandler
                .Setup(handler => handler.GenerateHashedPassword(command.Password, salt))
                .Returns(hashedPassword);

            _mockUserRepository
                .Setup(repo => repo.InsertAsync(user))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(hashedPassword, user.PasswordHash);
            Assert.Equal(Convert.ToBase64String(salt), user.Salt);
            Assert.Equal(UserRole.Guest, user.Role);
            _mockMapper.Verify(mapper => mapper.Map<User>(command), Times.Once);
            _mockPasswordHandler.Verify(handler => handler.GenerateHashingSaltValue(), Times.Once);
            _mockPasswordHandler.Verify(handler => handler.GenerateHashedPassword(command.Password, salt), Times.Once);
            _mockUserRepository.Verify(repo => repo.InsertAsync(user), Times.Once);
        }

        [Fact]
        public async Task Handle_PasswordHashingFails_ThrowsException()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PhoneNumber = "1234567890"
            };

            var user = new User { Id = Guid.NewGuid() };
            var salt = new byte[] { 1, 2, 3 };

            _mockMapper
                .Setup(mapper => mapper.Map<User>(command))
                .Returns(user);

            _mockPasswordHandler
                .Setup(handler => handler.GenerateHashingSaltValue())
                .Returns(salt);

            _mockPasswordHandler
                .Setup(handler => handler.GenerateHashedPassword(command.Password, salt))
                .Returns((string?)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(command, CancellationToken.None)
            );

            _mockMapper.Verify(mapper => mapper.Map<User>(command), Times.Once);
            _mockPasswordHandler.Verify(handler => handler.GenerateHashingSaltValue(), Times.Once);
            _mockPasswordHandler.Verify(handler => handler.GenerateHashedPassword(command.Password, salt), Times.Once);
            _mockUserRepository.Verify(repo => repo.InsertAsync(It.IsAny<User>()), Times.Never);
        }
    }
}
