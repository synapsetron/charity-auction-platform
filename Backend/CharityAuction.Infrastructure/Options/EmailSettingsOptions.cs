
using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Options
{
    public class EmailSettingsOptions
    {
        public const string SectionName = "EmailSettings";

        [Required(ErrorMessage = "SMTP server is required.")]
        public string SmtpServer { get; set; } = string.Empty;

        [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "Sender name is required.")]
        public string SenderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sender email is required.")]
        [EmailAddress(ErrorMessage = "Sender email must be a valid email address.")]
        public string SenderEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        public bool UseSSL { get; set; }
    }
}
