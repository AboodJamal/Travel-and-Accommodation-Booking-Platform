namespace TABP.Infrastructure.EmailServices.EmailService;

public class SmtpSettings
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
}