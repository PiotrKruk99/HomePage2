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

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
