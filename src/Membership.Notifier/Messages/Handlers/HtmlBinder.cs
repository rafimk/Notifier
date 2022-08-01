namespace Membership.Notifier.Services;

public static class HtmlBinder
{
    public static string Create(string firstDigit, string secondDigit, string thirdDigit, string fourthDigit)
    {
        string body = string.Empty;
        var templatePath = "otptemplate.html";
        using (StreamReader reader = new StreamReader(templatePath))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{firstDigit}", firstDigit);
        body = body.Replace("{secondDigit}", secondDigit);
        body = body.Replace("{thirdDigit}", thirdDigit);
        body = body.Replace("{fourthDigit}", fourthDigit);
        return body;
    }
}