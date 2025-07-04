﻿using CharityAuction.Application.Interfaces;
using CharityAuction.Infrastructure.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;


namespace CharityAuction.Application.Services.Email
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly SmtpClient _smtp;

        public SmtpClientWrapper(IOptions<EmailSettingsOptions> options)
        {
            var settings = options.Value;
            _smtp = new SmtpClient(settings.SmtpServer, settings.Port)
            {
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = settings.UseSSL
            };
        }

        public Task SendMailAsync(MailMessage message) => _smtp.SendMailAsync(message);

        public void Dispose() => _smtp.Dispose();
    }

}
