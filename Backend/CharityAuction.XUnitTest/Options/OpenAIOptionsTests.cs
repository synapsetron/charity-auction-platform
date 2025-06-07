using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class OpenAIOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new OpenAIOptions
            {
                ApiKey = "sk-very-secret-openai-key"
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MissingApiKey_ShouldFailValidation(string? apiKey)
        {
            var options = new OpenAIOptions
            {
                ApiKey = apiKey ?? string.Empty
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(OpenAIOptions.ApiKey)));
        }
    }
}
