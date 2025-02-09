using Moq;
using Microsoft.Extensions.Configuration;
using TABP.Hashing.PasswordUtils;

namespace TABP.Tests.UtilsTests
{
    public class PasswordHandlerTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly PasswordHandler _passwordHandler;

        public PasswordHandlerTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration
                .Setup(config => config["PasswordHandlerSaltSize"])
                .Returns("16"); // 16 bytes = 128 bits

            _mockConfiguration
                .Setup(config => config["PasswordHandlerTimeCost"])
                .Returns("3"); // Time cost for Argon2

            _mockConfiguration
                .Setup(config => config["PasswordHandlerSecret"])
                .Returns("supersecretkey"); // Secret key for Argon2

            _mockConfiguration
                .Setup(config => config["PasswordHandlerHashLength"])
                .Returns("32"); // 32 bytes = 256 bits

            _passwordHandler = new PasswordHandler(_mockConfiguration.Object);
        }

        [Fact]
        public void GenerateHashingSaltValue_ReturnsSaltOfCorrectSize()
        {
            // Act
            var salt = _passwordHandler.GenerateHashingSaltValue();

            // Assert
            Assert.NotNull(salt);
            Assert.Equal(16, salt.Length); // Salt size should match the configuration
        }

        [Fact]
        public void GenerateHashedPassword_ValidPassword_ReturnsHash()
        {
            // Arrange
            var password = "Password123!";
            var salt = _passwordHandler.GenerateHashingSaltValue();

            // Act
            var hash = _passwordHandler.GenerateHashedPassword(password, salt);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
        }

        [Fact]
        public void GenerateHashedPassword_InvalidPassword_ThrowsArgumentException()
        {
            // Arrange
            var salt = _passwordHandler.GenerateHashingSaltValue();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _passwordHandler.GenerateHashedPassword("", salt)
            );

            Assert.Throws<ArgumentException>(() =>
                _passwordHandler.GenerateHashedPassword(null, salt)
            );
        }

        [Fact]
        public void VerifyUserPassword_CorrectPassword_ReturnsTrue()
        {
            // Arrange
            var password = "Password123!";
            var salt = _passwordHandler.GenerateHashingSaltValue();
            var hashedPassword = _passwordHandler.GenerateHashedPassword(password, salt);

            // Act
            var result = _passwordHandler.VerifyUserPassword(password, hashedPassword, salt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyUserPassword_IncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var password = "Password123!";
            var incorrectPassword = "WrongPassword!";
            var salt = _passwordHandler.GenerateHashingSaltValue();
            var hashedPassword = _passwordHandler.GenerateHashedPassword(password, salt);

            // Act
            var result = _passwordHandler.VerifyUserPassword(incorrectPassword, hashedPassword, salt);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyUserPassword_InvalidHash_ReturnsFalse()
        {
            // Arrange
            var password = "Password123!";
            var salt = _passwordHandler.GenerateHashingSaltValue();
            var invalidHash = "invalidhash";

            // Act
            var result = _passwordHandler.VerifyUserPassword(password, invalidHash, salt);

            // Assert
            Assert.False(result);
        }
    }
}
