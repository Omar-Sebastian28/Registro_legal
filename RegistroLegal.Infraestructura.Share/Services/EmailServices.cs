using Microsoft.Extensions.Options;
using MimeKit;
using RegistroLegal.Core.Aplications.Dto.Email;
using RegistroLegal.Core.Domain.Settings;
using Microsoft.Extensions.Logging;
using RegistroLegal.Core.Aplications.Interfaces;

namespace RegistroLegal.Infraestructura.Share.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly MailSettings MailSettings;

        private readonly ILogger<EmailServices> Logger;

        public EmailServices(IOptions<MailSettings> mailSetting, ILogger<EmailServices> logger)
        {
            MailSettings = mailSetting.Value;
            Logger = logger;
        }

        public async Task SendAsync(EmailRequestDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.To))
            {
                dto.ToRange.Add(dto.To);
            }
            try
            {
                MimeMessage email = new()
                {
                    Sender = MailboxAddress.Parse(MailSettings.EmailFrom),
                    Subject = dto.Subject,
                    Date = DateTime.UtcNow.Date,
                };

                foreach (var toItem in dto.ToRange.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    email.To.Add(MailboxAddress.Parse(toItem));
                }

                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = dto.HtmlBdy
                };

                email.Body = bodyBuilder.ToMessageBody();

                using MailKit.Net.Smtp.SmtpClient smtpClient = new();
                await smtpClient.ConnectAsync(MailSettings.SmtpHost, MailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Logger.LogError($"{ex}: Error al enviar el correo: {ex.Message}");
            }
        }
    }
}
