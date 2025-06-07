using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class GoogleAuthOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new GoogleAuthOptions
            {
                ClientId = "google-client-id",
                ClientSecret = "google-client-secret"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Fact]
        public void MissingClientId_ShouldFailValidation()
        {
            var options = new GoogleAuthOptions
            {
                ClientId = "",
                ClientSecret = "google-client-secret"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(GoogleAuthOptions.ClientId)));
        }

        [Fact]
        public void MissingClientSecret_ShouldFailValidation()
        {
            var options = new GoogleAuthOptions
            {
                ClientId = "google-client-id",
                ClientSecret = ""
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(GoogleAuthOptions.ClientSecret)));
        }

        [Fact]
        public void MissingBothFields_ShouldFailValidation()
        {
            var options = new GoogleAuthOptions
            {
                ClientId = "",
                ClientSecret = ""
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(GoogleAuthOptions.ClientId)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(GoogleAuthOptions.ClientSecret)));
        }
    }
}
