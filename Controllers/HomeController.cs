using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using homePage2.Models;
using FluentEmail.Core;

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

    public async Task<IActionResult> Login()
    {
        var s = JsonOper.Read();
        ViewBag.message = "";
        if (s != null) ViewBag.message = s;

        switch (LiteDBOper.CheckAdmin())
        {
            case -1:
                ViewBag.message = "error connectinng to database";
                break;
            case 0:
                var email = await Email
                        .From("bill.gates@microsoft.com")
                        .To("p.kruk@liberezo.pl", "P Kruk")
                        .Subject("test")
                        .Body("to jest test wysyłki")
                        .SendAsync();
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
