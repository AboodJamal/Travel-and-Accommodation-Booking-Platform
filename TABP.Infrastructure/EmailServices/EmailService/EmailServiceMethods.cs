using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TABP.Infrastructure.EmailServices.EmailService;
using Infrastructure.EmailServices;

namespace TABP.Infrastructure.EmailServices
{
    public class EmailServiceMethods
    {
        private readonly SmtpSettings smptpDetails;

        public EmailServiceMethods(SmtpSettings smtpDetails)
        {
            smptpDetails = smtpDetails;
        }

        public async Task Send(MimeMessage mailMessage)
        {
            using var smtpClient = new SmtpClient();
            try
            {
                await smtpClient.ConnectAsync(smptpDetails.SmtpServer, smptpDetails.Port, true);
                smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await smtpClient.AuthenticateAsync(smptpDetails.UserName, smptpDetails.Password);

                await smtpClient.SendAsync(mailMessage);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
            finally
            {
                await smtpClient.DisconnectAsync(true);
                smtpClient.Dispose();
            }
        }

        public MimeMessage CreateEmailMessage(EmailMessageDetails Emailmessage, List<EmailAttachment> emailAttachments)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(smptpDetails.UserName, smptpDetails.From));
            mimeMessage.To.AddRange(Emailmessage.To);
            mimeMessage.Subject = Emailmessage.Subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = Emailmessage.Content
            };

            var multipart = new Multipart("mixed");
            mimeMessage.Body = multipart;

            multipart.Add(bodyBuilder.ToMessageBody());
            foreach (var attachment in emailAttachments)
            {
                var attachmentPart = new MimePart(attachment.ContentDataType)
                {
                    Content = new MimeContent(new MemoryStream(attachment.Data), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = attachment.FileName
                };

                multipart.Add(attachmentPart);
            }

            return mimeMessage;
        }
    }
}
