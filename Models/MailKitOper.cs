using MailKit.Net.Smtp;
using MimeKit;

namespace homePage2.Models;

public static class MailKitOper
{
    private static ResultMsg SendEmail(MimeMessage email)
    {
        JsonOper.Fields? fields = JsonOper.ReadFile();
        if (fields == null) return new ResultMsg(false, 0, "cannot get mail data", ResultMsg.ResultType.danger);

        email.To.Add(MailboxAddress.Parse(fields.email));
        email.From.Add(MailboxAddress.Parse(fields.mailLogin));

        var smtp = new SmtpClient();

        try
        {
            smtp.Connect(fields.mailHost, fields.mailPort ?? 0, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(fields.mailLogin, fields.mailPassword);
            smtp.Timeout = 3000;
            smtp.Send(email);
        }
        catch (Exception)
        {
            return new ResultMsg(false, 0, "cannot send registration email", ResultMsg.ResultType.danger);
        }
        finally
        {
            smtp.Disconnect(true);
        }

        return new ResultMsg(true, 0, "registration email send", ResultMsg.ResultType.success);
    }

    public static ResultMsg SendRegistrationEmail(IConfiguration config)
    {
        var addToBaseResult = LiteDBOper.AddAdmin();

        if (!addToBaseResult.Result)
        {
            return addToBaseResult;
        }

        var email = new MimeMessage();
        email.Subject = "registration mail from HomePage";

        string body = @"<div style=""background-color: white; color: black; padding: 5px;"">";
        body += @"<h3>Witamy w rejestracji do HomePage</h3>";
        body += @"<p>
                    Aby przejść do okna tworzenia hasła nowego konta administracyjnego kliknij
                    poniższy link lub przekopiuj go do paska adresu przeglądarki.
                </p>";
        body += @"<a href=""" + AppSettingsOper.GetHostPath(config) + @"Login?authString=";
        body += addToBaseResult.MsgText;
        body += @""" target=""_blank"">" + AppSettingsOper.GetHostPath(config) + @"Login?id=";
        body += addToBaseResult.MsgText + @"</a>";
        body += @"<p>
                    W razie problemów z rejestracją prosimy o kontakt z Liberezo.
                </p></div>";

        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        return SendEmail(email);
    }
}