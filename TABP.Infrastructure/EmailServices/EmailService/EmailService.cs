using Infrastructure.EmailServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TABP.Infrastructure.EmailServices.EmailService
{
    public class EmailService : EmailServiceInterface
    {
        private readonly SmtpSettings smptpDetails;
        private readonly IConfiguration _configuration;
        private readonly EmailServiceMethods _emailServiceMethods;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            smptpDetails = new SmtpSettings
            {
                From = _configuration["EmailConfigurationFrom"]!,
                SmtpServer = _configuration["EmailConfigurationSmtpServer"]!,
                UserName = _configuration["EmailConfigurationUserName"]!,
                Port = int.Parse(_configuration["EmailConfigurationPort"]!),
                Password = _configuration["EmailConfigurationPassword"]!
            };

            _emailServiceMethods = new EmailServiceMethods(smptpDetails);
        }

        public async Task SendEmailAsync(EmailMessageDetails Emailmessage, List<EmailAttachment> emailAttachments)
        {
            var emailMessage = _emailServiceMethods.CreateEmailMessage(Emailmessage, emailAttachments);
            await _emailServiceMethods.Send(emailMessage);
        }
    }
}
