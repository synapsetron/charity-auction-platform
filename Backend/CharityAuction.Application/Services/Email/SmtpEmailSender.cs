using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading;
using CharityAuction.Infrastructure.Options;

namespace CharityAuction.Infrastructure.Services
{
    /// <summary>
    /// Service responsible for sending emails via SMTP.
    /// </summary>
    public class SmtpEmailSender : IEmailSender
    {
        private readonly EmailSettingsOptions _settings;
        private readonly ILogger<SmtpEmailSender> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpEmailSender"/> class.
        /// </summary>
        public SmtpEmailSender(IOptions<EmailSettingsOptions> settings, ILogger<SmtpEmailSender> logger)
        {
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Recipient email cannot be null or empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Email subject cannot be null or empty.", nameof(subject));
            if (string.IsNullOrWhiteSpace(htmlMessage))
                throw new ArgumentException("Email body cannot be null or empty.", nameof(htmlMessage));

            try
            {
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                using var smtpClient = new SmtpClient(_settings.SmtpServer, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = _settings.UseSSL,
                    Timeout = 10000 // milliseconds
                };

                _logger.LogInformation("Sending email to {Email}...", email);
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email successfully sent to {Email}.", email);
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP error occurred while sending email to {Email}.", email);
                throw new InvalidOperationException($"SMTP error: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while sending email to {Email}.", email);
                throw;
            }
        }
    }
}
