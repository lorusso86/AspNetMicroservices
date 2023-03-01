using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastracture;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            EmailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public EmailSettings EmailSettings { get; set; }
        public ILogger<EmailService> Logger { get; set; }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(EmailSettings.ApiKey);

            var from = new EmailAddress(EmailSettings.FromAddress);
            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;


            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);

            Logger.LogInformation("Mail Sent");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            Logger.LogError("Email failed");

            return false;
        }
    }
}
