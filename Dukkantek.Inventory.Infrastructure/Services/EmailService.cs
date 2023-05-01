using System.Threading.Tasks;
using Dukkantek.Inventory.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Dukkantek.Inventory.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        private Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress($"{Options.SendingEmail}", $"{Options.SendingName}"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
                TemplateId = Options.TemplateId,
                ReplyTo = new EmailAddress(Options.ReplyToEmail)
            };
            msg.AddTo(new EmailAddress(email));

            if (Options.AllowEmail)
            {
                return client.SendEmailAsync(msg);
            }

            return Task.CompletedTask;
        }
    }
}
