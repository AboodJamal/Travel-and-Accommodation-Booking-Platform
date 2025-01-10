using MimeKit;

namespace Infrastructure.EmailServices;

public class EmailMessageDetails
{
    public List<MailboxAddress> To { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }

    public EmailMessageDetails(IEnumerable<string> to, string subject, string content)
    {
        To = to.Select(email => new MailboxAddress("Recipient", email)).ToList();
        Subject = subject;
        Content = content;
    }
}