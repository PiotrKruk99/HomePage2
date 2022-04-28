using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using homePage2.Models;

namespace homePage2.Controllers;

public class NewsController : Controller
{
    [Route("/NewsEdit")]
    [Authorize(Roles = "admin")]
    public IActionResult NewsEdit()
    {
        return View();
    }

    [Route("/AddNews")]
    [Authorize(Roles = "admin")]
    public IActionResult AddNews()
    {
        string title = Request.Form["title"];
        string content = Request.Form["content"];

        if (title.Equals("") || content.Equals(""))
        {
            ViewBag.message = BootstrapOper.Alert(new ResultMsg(false, "title or content is empty", ResultMsg.ResultType.warning));
            return View();
        }
        
        return View();
    }
}