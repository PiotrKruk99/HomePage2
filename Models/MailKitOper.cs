//using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace homePage2.Models;

public static class MailKitOper
{
    public static bool SendRegistrationEmail()
    {
        JsonOper.Field? field = JsonOper.ReadField();
        if (field == null) return false;

        var email = new MimeMessage();
        email.To.Add(MailboxAddress.Parse(field.email));
        email.From.Add(MailboxAddress.Parse(field.mailLogin));
        email.Subject = "test";
        email.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = "to jest test wysyłki wiadomości przez bibliotekę MailKit" };

        var smtp = new SmtpClient();

        try
        {
            smtp.Connect(field.mailHost, field.mailPort ?? 0, MailKit.Security.SecureSocketOptions.Auto);
            smtp.Authenticate(field.mailLogin, field.mailPassword);
            smtp.Timeout = 3000;
            smtp.Send(email);
        }
        catch (Exception) { return false; }
        finally
        {
            smtp.Disconnect(true);
        }

        return true;
    }
}