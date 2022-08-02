using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Membership.Notifier.EmailService;

public class EmailService : IEmailService
{
    public EmailService()
    {
    }

    public async Task Send(string to, string subject, string html, string from = null)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? "uaekmcc@outlook.com"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        var smtp = new SmtpClient();
        smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("uaekmcc@outlook.com", "Kmcc@7425403");

        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}