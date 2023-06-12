using AuctionAngular.Dtos;
using AuctionAngular.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;

namespace AuctionAngular.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmailAsync(MailRequestDto dto)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(this.mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(dto.ToEmail));
            email.Subject = dto.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = dto.Body;
            email.Body = builder.ToMessageBody();
            using var smpt = new SmtpClient();
            smpt.Connect(this.mailSettings.Host, this.mailSettings.Port, SecureSocketOptions.StartTls);
            smpt.Authenticate(this.mailSettings.Mail, this.mailSettings.Password);
            var response = await smpt.SendAsync(email);
            smpt.Dispose();

            if (response != null)
            {
                return true;
            }

            return false;

        }
    }
}