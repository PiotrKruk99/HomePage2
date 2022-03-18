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

    public IActionResult Login()
    {
        var s = JsonOper.ReadEmail();
        ViewBag.message = "";
        if (s != null) ViewBag.message = s;

        switch (LiteDBOper.CheckAdmin())
        {
            case -1:
                ViewBag.message = "error connectinng to database";
                break;
            case 0:
                ViewBag.message = "registration email was send";
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
