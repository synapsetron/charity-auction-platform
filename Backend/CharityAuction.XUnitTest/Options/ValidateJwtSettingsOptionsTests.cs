using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class ValidateJwtSettingsOptionsTests
    {
        private readonly ValidateJwtSettingsOptions _validator = new();

        [Fact]
        public void AllValid_ShouldReturnSuccess()
        {
            var options = new JwtSettingsOptions
            {
                Secret = "secret",
                AccessTokenExpirationMinutes = 60,
                RefreshTokenExpirationDays = 7,
                Issuer = "issuer",
                Audience = "audience"
            };

            var result = _validator.Validate(null, options);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public void MissingSecret_ShouldReturnFailure()
        {
            var options = GetValidOptions();
            options.Secret = "";

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("JWT Secret is required.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1441)]
        public void InvalidAccessTokenExpiration_ShouldReturnFailure(int invalidValue)
        {
            var options = GetValidOptions();
            options.AccessTokenExpirationMinutes = invalidValue;

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("AccessTokenExpirationMinutes must be between 1 and 1440.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(366)]
        public void InvalidRefreshTokenExpiration_ShouldReturnFailure(int invalidValue)
        {
            var options = GetValidOptions();
            options.RefreshTokenExpirationDays = invalidValue;

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("RefreshTokenExpirationDays must be between 1 and 365.");
        }

        [Fact]
        public void MissingIssuer_ShouldReturnFailure()
        {
            var options = GetValidOptions();
            options.Issuer = "";

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("JWT Issuer is required.");
        }

        [Fact]
        public void MissingAudience_ShouldReturnFailure()
        {
            var options = GetValidOptions();
            options.Audience = "";

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("JWT Audience is required.");
        }

        [Fact]
        public void MultipleFailures_ShouldReturnAllMessages()
        {
            var options = new JwtSettingsOptions(); // все значения по умолчанию — невалидные

            var result = _validator.Validate(null, options);

            result.Failed.Should().BeTrue();
            result.FailureMessage.Should().Contain("JWT Secret is required.");
            result.FailureMessage.Should().Contain("AccessTokenExpirationMinutes must be between 1 and 1440.");
            result.FailureMessage.Should().Contain("RefreshTokenExpirationDays must be between 1 and 365.");
            result.FailureMessage.Should().Contain("JWT Issuer is required.");
            result.FailureMessage.Should().Contain("JWT Audience is required.");
        }

        private static JwtSettingsOptions GetValidOptions() => new()
        {
            Secret = "secret",
            AccessTokenExpirationMinutes = 60,
            RefreshTokenExpirationDays = 7,
            Issuer = "issuer",
            Audience = "audience"
        };
    }
}
