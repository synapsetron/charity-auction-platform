using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class JwtSettingsOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new JwtSettingsOptions
            {
                Secret = "supersecret",
                AccessTokenExpirationMinutes = 60,
                RefreshTokenExpirationDays = 30,
                Issuer = "issuer",
                Audience = "audience"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Fact]
        public void MissingSecret_ShouldFailValidation()
        {
            var options = new JwtSettingsOptions
            {
                Secret = "",
                AccessTokenExpirationMinutes = 60,
                RefreshTokenExpirationDays = 30,
                Issuer = "issuer",
                Audience = "audience"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(x => x.MemberNames.Contains(nameof(JwtSettingsOptions.Secret)));
        }
    }
}
