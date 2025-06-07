using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading;
using CharityAuction.Infrastructure.Options;
using CharityAuction.Application.Interfaces;

namespace CharityAuction.Infrastructure.Services
{
    /// <summary>
    /// Service responsible for sending emails via SMTP.
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettingsOptions _settings;
        private readonly ILogger<SmtpEmailSender> _logger;
        private readonly Func<ISmtpClient> _smtpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailSender"/> class.
        /// </summary>
        public SmtpEmailSender(IOptions<EmailSettingsOptions> settings,
         ILogger<SmtpEmailSender> logger,
         Func<ISmtpClient> smtpClientFactory)
        {
            _settings = settings.Value;
            _logger = logger;
            _smtpClientFactory = smtpClientFactory;
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Recipient email is required.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("Subject is required.", nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlMessage)) throw new ArgumentException("Message body is required.", nameof(htmlMessage));

            using var smtp = _smtpClientFactory();

            var message = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            message.To.Add(email);

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP error");
                throw new InvalidOperationException("SMTP error: " + ex.Message, ex);
            }
        }
    }
}
