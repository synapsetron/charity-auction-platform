using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class LiqPayOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new LiqPayOptions
            {
                PublicKey = "public123",
                PrivateKey = "private456",
                ResultUrl = "https://example.com/result",
                ServerUrl = "https://example.com/server"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Fact]
        public void MissingRequiredFields_ShouldFailValidation()
        {
            var options = new LiqPayOptions(); 

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.PublicKey)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.PrivateKey)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.ResultUrl)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.ServerUrl)));
        }

        [Theory]
        [InlineData("invalid-url")]
        [InlineData("http:/broken.com")]
        [InlineData("fp://wrong.protocol.com")]
        public void InvalidUrls_ShouldFailValidation(string invalidUrl)
        {
            var options = new LiqPayOptions
            {
                PublicKey = "public",
                PrivateKey = "private",
                ResultUrl = invalidUrl,
                ServerUrl = invalidUrl
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.ResultUrl)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(LiqPayOptions.ServerUrl)));
        }
    }
}
