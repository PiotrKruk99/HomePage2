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


        // SmtpSender sender = new SmtpSender(() => new SmtpClient()
        // {
        //     Host = field.mailHost ?? "",
        //     Port = field.mailPort ?? 0,
        //     UseDefaultCredentials = false,
        //     Credentials = new NetworkCredential(field.mailLogin, field.mailPassword),
        //     DeliveryMethod = SmtpDeliveryMethod.Network,
        //     EnableSsl = true
        // });

        // Email.DefaultSender = sender;
        // Email.From("biuro@liberezo.pl").To(field.email).Subject("test")
        //     .Body("to jest test FluentEmail").Send();

        return true;
    }
}