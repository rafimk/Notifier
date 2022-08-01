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
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? "muhammed.rafi@membership-app.me"));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect("mail.privateemail.com", 465, SecureSocketOptions.SslOnConnect);
        smtp.Authenticate("muhammed.rafi@membership-app.me", "Mem@4296326");
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}