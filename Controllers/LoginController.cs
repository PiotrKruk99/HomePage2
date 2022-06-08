using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homePage2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace homePage2.Controllers;

public class LoginController : Controller
{
    private IConfiguration _config;

    public LoginController(IConfiguration config)
    {
        _config = config;
    }

    private static (string admin, string) roles = (admin: "admin", "");

    [Route("/Login")]
    [HttpGet]
    public IActionResult Login(string authString = "")
    {
        if (HttpContext.User.Identity == null ? false : HttpContext.User.Identity.IsAuthenticated)
            return Redirect("/NewsEdit");

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

    [HttpPost]
    public IActionResult SetPassword()
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

    [HttpPost]
    public async Task<IActionResult> CheckAuthentication()
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

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, login));
        claims.Add(new Claim("login", login));
        claims.Add(new Claim(ClaimTypes.Role, roles.admin));
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(claimsPrincipal);

        return Redirect("/NewsEdit");
    }

    [Route("/Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}