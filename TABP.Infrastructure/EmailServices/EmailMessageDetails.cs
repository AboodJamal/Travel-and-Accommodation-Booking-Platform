using MimeKit;

namespace Infrastructure.EmailServices;

public class EmailMessageDetails
{
    public List<MailboxAddress> To { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }

    public EmailMessageDetails(IEnumerable<string> to, string subject, string content)
    {
        To = new List<MailboxAddress>();
        To.AddRange(to.Select(email => new MailboxAddress("email", email)));
        Subject = subject;
        Content = content;
    }
}