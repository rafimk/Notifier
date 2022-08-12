using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Membership.Notifier.EmailService;

public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;
    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
    }

    public async Task Send(string to, string subject, string html, string from = null)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailOptions.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            var smtp = new SmtpClient();
            smtp.Connect(_emailOptions.Smtp, _emailOptions.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailOptions.AuthenticatedUser, _emailOptions.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
       
    }
}