using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net.Mail;

namespace homePage2.Models;

public static class FluentOper {
    public static bool SendRegistrationEmail()
    {
        SmtpSender sender = new SmtpSender(() => new SmtpClient()
        {
            
        });
        return false;
    }
}