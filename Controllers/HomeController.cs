using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homePage2.Models;

namespace homePage2.Controllers;

public class HomeController : Controller
{
    private IConfiguration _config;

    public HomeController(IConfiguration config)
    {
        _config = config;
    }

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

        //ViewBag.emailConfirmed = false;
        var isAdmin = LiteDBOper.CheckAdmin();

        switch (isAdmin.ErrCode)
        {
            case -1: //database error
                ViewBag.message = BootstrapOper.Alert(isAdmin);
                break;

            case 0: //admin not present in database or admin entry expired
                ViewBag.message = BootstrapOper.Alert(MailKitOper.SendRegistrationEmail(_config));
                break;

            case 1: //admin present in database
                var adminAuthResult = LiteDBOper.CheckAdminsAuthString(authString);

                if (adminAuthResult.Result)
                {
                    ViewBag.authInit = true;
                }
                else
                {
                    ViewBag.message = BootstrapOper.Alert(adminAuthResult);
                }
                break;

            default:
                break;
        }
        //ViewBag.message = authString + "|" + authString.Length;
        //ViewBag.message = (new AppSettingsOper(_config)).GetHostPath();

        return View();
    }

    [HttpPost]
    public IActionResult Login()
    {
        return Redirect("Login");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
