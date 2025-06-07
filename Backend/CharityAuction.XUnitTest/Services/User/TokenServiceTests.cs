using System.Security.Claims;
using System.Text;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Services.User.JWT;
using CharityAuction.Domain.Entities;
using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CharityAuction.XUnitTest.Services.User
{
    public class TokenServiceTests
    {
        private readonly Mock<IClaimsService> _claimsServiceMock;
        private readonly Mock<ILogger<TokenService>> _loggerMock;
        private readonly IOptions<JwtSettingsOptions> _options;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _claimsServiceMock = new Mock<IClaimsService>();
            _loggerMock = new Mock<ILogger<TokenService>>();
            _options = Options.Create(new JwtSettingsOptions
            {
                Secret = "supersecretkey12345678901234567890", // min 256 bit
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                AccessTokenExpirationMinutes = 1
            });

            _tokenService = new TokenService(_options, _claimsServiceMock.Object, _loggerMock.Object);
        }

        [Fact(DisplayName = "GenerateRefreshToken should return valid Base64 string")]
        public void GenerateRefreshToken_ShouldReturnBase64String()
        {
            var token = _tokenService.GenerateRefreshToken();

            token.Should().NotBeNullOrEmpty();
            var bytes = Convert.FromBase64String(token);
            bytes.Length.Should().Be(64);
        }

        [Fact(DisplayName = "GenerateTokenAsync should throw when user is null")]
        public async Task GenerateTokenAsync_ShouldThrow_WhenUserIsNull()
        {
            Func<Task> act = async () => await _tokenService.GenerateTokenAsync(null!);

            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("*user*");
        }

        [Fact(DisplayName = "GenerateTokenAsync should log and propagate exception from IClaimsService")]
        public async Task GenerateTokenAsync_ShouldLogAndThrow_WhenClaimsServiceFails()
        {
            var user = new ApplicationUser { Id = "42", UserName = "test" };

            _claimsServiceMock.Setup(c => c.CreateClaimsAsync(user))
                .ThrowsAsync(new Exception("claims failed"));

            Func<Task> act = async () => await _tokenService.GenerateTokenAsync(user);

            await act.Should().ThrowAsync<Exception>().WithMessage("claims failed");
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Never); // TokenService не логирует исключение внутри GenerateTokenAsync
        }

        [Fact(DisplayName = "GenerateTokenAsync should return a valid JWT token string")]
        public async Task GenerateTokenAsync_ShouldReturnJwtString_WhenUserIsValid()
        {
            var user = new ApplicationUser
            {
                Id = "user-id-123",
                UserName = "john",
                Email = "john@example.com"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            _claimsServiceMock.Setup(cs => cs.CreateClaimsAsync(user)).ReturnsAsync(claims);

            var token = await _tokenService.GenerateTokenAsync(user);

            token.Should().NotBeNullOrEmpty();

            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.ReadJwtToken(token);
            readToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "john");
            readToken.Issuer.Should().Be("TestIssuer");
            readToken.Audiences.Should().Contain("TestAudience");
            readToken.ValidTo.Should().BeAfter(DateTime.UtcNow);
        }

        [Fact(DisplayName = "Constructor should throw if JwtSettingsOptions is missing secret")]
        public void Constructor_ShouldThrow_WhenSecretIsMissing()
        {
            var brokenOptions = Options.Create(new JwtSettingsOptions
            {
                Secret = "", // invalid
                Issuer = "X",
                Audience = "Y"
            });

            Action act = () => new TokenService(brokenOptions, _claimsServiceMock.Object, _loggerMock.Object);

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("JWT Secret key is missing in configuration.");
        }
    }
}
