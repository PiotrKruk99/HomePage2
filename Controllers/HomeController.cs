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

    [Route("/News")]
    public IActionResult News()
    {
        return View();
    }

    [Route("/Login")]
    [HttpGet]
    public IActionResult Login(string authString = "")
    {
        var isAdmin = LiteDBOper.CheckAdminExist();

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
                    ViewBag.requestPath = Request.Path + Request.QueryString;
                }
                else
                {
                    ViewBag.message = BootstrapOper.Alert(adminAuthResult);
                }
                break;

            case 2: //admin's password exists
                ViewBag.emailConfirmed = true;
                break;

            default:
                break;
        }

        return View();
    }

    [Route("/SetPass")]
    [HttpPost]
    public IActionResult SetPass()
    {
        string pass1 = Request.Form["password1"];
        string pass2 = Request.Form["password2"];
        string requestPath = Request.Form["sendBtn"];

        if (pass1.Equals(pass2) && !pass1.Equals(string.Empty))
        {
            LiteDBOper.SetAdminPassword(pass1);
            return Redirect("/Login");
        }
        else
        {
            TempData["message"] = BootstrapOper.Alert(new ResultMsg(false, "passwords are empty or not the same", ResultMsg.ResultType.warning));
            return Redirect(requestPath);
        }
    }

    [Route("/CheckAuthentication")]
    [HttpPost]
    public IActionResult CheckAuthentication()
    {
        string login = Request.Form["login"];
        string password = Request.Form["password"];

        if (login == string.Empty || password == string.Empty)
        {
            TempData["message"] = BootstrapOper.Alert(new ResultMsg(false, "login and passwords couldn't be empty", ResultMsg.ResultType.warning));
            return Redirect("/Login");
        }

        var checkAuthentication = LiteDBOper.CheckUsersAuthentication(login, password);
        if (!checkAuthentication.Result)
        {
            TempData["message"] = BootstrapOper.Alert(checkAuthentication);
            return Redirect("/Login");
        }

        TempData["message"] = BootstrapOper.Alert(new ResultMsg(true, "dane logowania poprawne", ResultMsg.ResultType.success));
        return Redirect("/Login");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
