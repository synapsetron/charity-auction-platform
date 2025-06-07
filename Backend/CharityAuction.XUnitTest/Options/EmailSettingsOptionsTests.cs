using CharityAuction.Infrastructure.Options;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public class EmailSettingsOptionsTests
    {
        [Fact]
        public void ValidOptions_ShouldPassValidation()
        {
            var options = new EmailSettingsOptions
            {
                SmtpServer = "smtp.example.com",
                Port = 587,
                SenderName = "Auction Service",
                SenderEmail = "noreply@charity.org",
                Username = "smtpuser",
                Password = "smtppass",
                UseSSL = true
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().BeEmpty();
        }

        [Fact]
        public void MissingRequiredFields_ShouldFailValidation()
        {
            var options = new EmailSettingsOptions(); 

            var results = ValidationHelper.ValidateObject(options);

            results.Should().Contain(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.SmtpServer)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.SenderName)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.SenderEmail)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.Username)));
            results.Should().Contain(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.Password)));
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("missingatsign.com")]
        [InlineData("missingdomain@")]
        public void InvalidEmailFormat_ShouldFailValidation(string invalidEmail)
        {
            var options = new EmailSettingsOptions
            {
                SmtpServer = "smtp.example.com",
                Port = 587,
                SenderName = "Auction Service",
                SenderEmail = invalidEmail,
                Username = "smtpuser",
                Password = "smtppass",
                UseSSL = true
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.SenderEmail)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(70000)]
        public void InvalidPort_ShouldFailValidation(int invalidPort)
        {
            var options = new EmailSettingsOptions
            {
                SmtpServer = "smtp.example.com",
                Port = invalidPort,
                SenderName = "Auction Service",
                SenderEmail = "noreply@charity.org",
                Username = "smtpuser",
                Password = "smtppass",
                UseSSL = true
            };

            var results = ValidationHelper.ValidateObject(options);

            results.Should().ContainSingle(r => r.MemberNames.Contains(nameof(EmailSettingsOptions.Port)));
        }
    }
}
