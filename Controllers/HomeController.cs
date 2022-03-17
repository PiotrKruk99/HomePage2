using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homePage2.Models;
using System.Net.Mail;

namespace homePage2.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult News()
    {
        return View();
    }

    public IActionResult Login()
    {
        var s = JsonOper.Read("email");
        ViewBag.message = "";
        //if (s != null) ViewBag.message = s;

        switch (LiteDBOper.CheckAdmin())
        {
            case -1:
                ViewBag.message = "error connectinng to database";
                break;
            case 0:
                MailMessage msg = new MailMessage("biuro@liberezo.pl", "p.kruk@liberezo.pl", "test", "to jest test wysyłki maila");
                SmtpClient smtp = new SmtpClient("smtp.webio.pl", 465);
                smtp.Send(msg);
                ViewBag.message = "registration email was post";
                break;
            case 1:
                ViewBag.message = "admin istnieje";
                break;
            default:
                break;
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
