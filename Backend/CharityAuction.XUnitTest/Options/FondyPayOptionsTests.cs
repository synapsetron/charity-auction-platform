using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class FondyPayOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new FondyPayOptions
            {
                MerchantId = 123456,
                SecretKey = "supersecretkey",
                ResultUrl = "https://example.com/result",
                ServerUrl = "https://example.com/server"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Fact]
        public void MissingRequiredFields_ShouldFailValidation()
        {
            var options = new FondyPayOptions(); 

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.MerchantId)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.SecretKey)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.ResultUrl)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.ServerUrl)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void InvalidMerchantId_ShouldFailValidation(int invalidId)
        {
            var options = new FondyPayOptions
            {
                MerchantId = invalidId,
                SecretKey = "secret",
                ResultUrl = "https://example.com/result",
                ServerUrl = "https://example.com/server"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(FondyPayOptions.MerchantId)));
        }

        [Theory]
        [InlineData("not-a-url")]
        [InlineData("ft://example.com")]
        [InlineData("http:/broken.url")]
        public void InvalidUrls_ShouldFailValidation(string invalidUrl)
        {
            var options = new FondyPayOptions
            {
                MerchantId = 123456,
                SecretKey = "secret",
                ResultUrl = invalidUrl,
                ServerUrl = invalidUrl
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.ResultUrl)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(FondyPayOptions.ServerUrl)));
        }
    }
}
