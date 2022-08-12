namespace Membership.Notifier;

public class EmailOptions
{
    public string EmailFrom { get; set; }
    public string Smtp { get; set; }
    public int Port { get; set; }
    public string AuthenticatedUser { get; set; }
    public string Password { get; set; }
}