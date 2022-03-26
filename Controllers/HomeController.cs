using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homePage2.Models;

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

    [HttpGet]
    public IActionResult Login(string authString = "")
    {
        // var s = JsonOper.ReadEmail();
        // ViewBag.message = "";
        // if (s != null) ViewBag.message = s;

        ViewBag.emailCompared = true;

        switch (LiteDBOper.CheckAdmin())
        {
            case -1:
                ViewBag.message = BootstrapOper.BootstrapAlert(new ResultMsg(false, "error connecting to database", ResultMsg.ResultType.danger));
                break;
            case 0:
                ViewBag.emailCompared = false;
                ViewBag.message = BootstrapOper.BootstrapAlert(MailKitOper.SendRegistrationEmail());
                break;
            case 1:
                ViewBag.message = "admin istnieje";
                break;
            default:
                break;
        }
        //ViewBag.message = authString + "|" + authString.Length;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
