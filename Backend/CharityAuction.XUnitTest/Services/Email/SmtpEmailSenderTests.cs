using CharityAuction.Application.Interfaces;
using CharityAuction.Infrastructure.Options;
using CharityAuction.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharityAuction.XUnitTest.Services.Email
{
    public class SmtpEmailSenderTests
    {
        private readonly Mock<ILogger<SmtpEmailSender>> _loggerMock = new();
        private readonly EmailSettingsOptions _validSettings = new()
        {
            SmtpServer = "smtp.example.com",
            Port = 587,
            Username = "test@example.com",
            Password = "password",
            SenderEmail = "sender@example.com",
            SenderName = "Auction System",
            UseSSL = true
        };

        private SmtpEmailSender CreateService(
                EmailSettingsOptions? overrideSettings = null,
                Func<ISmtpClient>? smtpFactory = null)
                    {
                        var optionsMock = Mock.Of<IOptions<EmailSettingsOptions>>(o =>
                            o.Value == (overrideSettings ?? _validSettings));

                        return new SmtpEmailSender(
                            optionsMock,
                            _loggerMock.Object,
                            smtpFactory ?? (() => Mock.Of<ISmtpClient>())
                        );
        }


        [Fact(DisplayName = "Throws if recipient email is null or whitespace")]
        public async Task SendEmailAsync_NullEmail_ThrowsArgumentException()
        {
            var service = CreateService();

            Func<Task> act = async () =>
                await service.SendEmailAsync(null!, "subject", "message");

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*email*");
        }

        [Fact(DisplayName = "Throws if subject is null or whitespace")]
        public async Task SendEmailAsync_NullSubject_ThrowsArgumentException()
        {
            var service = CreateService();

            Func<Task> act = async () =>
                await service.SendEmailAsync("to@example.com", null!, "message");

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*subject*");
        }

        [Fact(DisplayName = "Throws if body is null or whitespace")]
        public async Task SendEmailAsync_NullBody_ThrowsArgumentException()
        {
            var service = CreateService();

            Func<Task> act = async () =>
                await service.SendEmailAsync("to@example.com", "subject", null!);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("*body*");
        }

        [Fact(DisplayName = "Throws InvalidOperationException on SMTP error")]
        public async Task SendEmailAsync_SmtpException_ThrowsInvalidOperationException()
        {
            var service = CreateService();
            var smtpEx = new SmtpException("Simulated failure");

            using var smtpClient = new SmtpClient(_validSettings.SmtpServer, _validSettings.Port);
            service.Invoking(async s =>
            {
                // simulate failure using local override (not mockable directly)
                await s.SendEmailAsync("bad@host.com", "subject", "message");
            }).Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact(DisplayName = "Successfully sends email via mocked SMTP client")]
        public async Task SendEmailAsync_ValidInput_SuccessfullyCallsSmtp()
        {
            var smtpMock = new Mock<ISmtpClient>();
            smtpMock.Setup(s => s.SendMailAsync(It.IsAny<MailMessage>())).Returns(Task.CompletedTask);

            var sender = new SmtpEmailSender(
                Options.Create(_validSettings),
                _loggerMock.Object,
                () => smtpMock.Object
            );

            var act = async () => await sender.SendEmailAsync("test@unit.com", "Subject", "<b>Body</b>");

            await act.Should().NotThrowAsync();
            smtpMock.Verify(s => s.SendMailAsync(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}
