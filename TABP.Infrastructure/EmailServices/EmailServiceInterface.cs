namespace Infrastructure.EmailServices;

public interface EmailServiceInterface
{
    public Task SendEmailAsync(EmailMessageDetails message, List<EmailAttachment> attachments);
}