using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Net;
using homePage2.Models;

namespace homePage2.Models;

public static class FluentOper {
    public static bool SendRegistrationEmail()
    {
        JsonOper.Field? field = JsonOper.ReadField();
        if (field == null) return false;

        SmtpSender sender = new SmtpSender(() => new SmtpClient()
        {
            Host = field.mailHost ?? "",
            Port = field.mailPort ?? 0,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(field.mailLogin, field.mailPassword),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = true
        });

        Email.DefaultSender = sender;
        Email.From("biuro@liberezo.pl").To(field.email).Subject("test")
            .Body("to jest test FluentEmail").Send();
            
        return true;
    }
}