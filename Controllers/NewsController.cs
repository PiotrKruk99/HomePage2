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
        ViewBag.news = LiteDBOper.GetAllArticles();

        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult DeleteArticle()
    {
        int deleteId = Convert.ToInt32(Request.Form["deleteId"]);
        LiteDBOper.DeleteArticle(deleteId);

        return Redirect("/NewsEdit");
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult EditArticle()
    {
        int editId = Convert.ToInt32(Request.Form["editId"]);
        ViewBag.article = LiteDBOper.GetArticle(editId);
        return AddArticleGet();
    }

    [Authorize(Roles = "admin")]
    public IActionResult AddArticleGet()
    {
        return View("AddArticle");
    }

    [Authorize(Roles = "admin")]
    public IActionResult AddArticlePost()
    {
        string title = Request.Form["title"];
        string content = Request.Form["content"];

        if (title.Equals("") || content.Equals(""))
        {
            ViewBag.message = BootstrapOper.Alert(new ResultMsg(false, "title or content is empty", ResultMsg.ResultType.warning));
            return View("AddArticle");
        }

        LiteDBOper.AddArticle(new Article {Title = title, Content = content});
        
        return Redirect("/NewsEdit");
    }
}