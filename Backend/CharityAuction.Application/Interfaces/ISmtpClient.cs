using System.Net.Mail;

namespace CharityAuction.Application.Interfaces
{
    public interface ISmtpClient : IDisposable
    {
        Task SendMailAsync(MailMessage message);
    }
}
