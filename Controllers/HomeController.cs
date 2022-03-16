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

    public IActionResult Login()
    {
        var s = JsonOper.Read("email");
        ViewBag.test = "";
        if (s != null) ViewBag.test = s;

        switch (LiteDBOper.CheckAdmin())
        {
            case -1:
                ViewBag.test = "błąd komunikacji z bazą";
                break;
            case 0:
                ViewBag.test = "admin nie istnieje";
                break;
            case 1:
                ViewBag.test = "admin istnieje";
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
