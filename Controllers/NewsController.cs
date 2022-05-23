using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using homePage2.Models;
using System.Text.RegularExpressions;

namespace homePage2.Controllers;

public class NewsController : Controller
{
    [Route("/NewsEdit")]
    [Authorize(Roles = "admin")]
    public IActionResult NewsEdit()
    {
        //ViewBag.news = LiteDBOper.GetAllArticles();
        var news  = LiteDBOper.GetAllArticles();

        var newsList = news == null ? new List<Article>() : new List<Article>(news);

        for (var i = 0; i < newsList.Count; i++)
        {
            newsList[i].Content = Regex.Replace(newsList[i].Content, "<.*?>", string.Empty);
            newsList[i].Content = newsList[i].Content.Replace(Environment.NewLine, "<br>");
        }

        ViewBag.news = newsList;

        return View();
    }

    [HttpGet("/AddArticle")]
    [Authorize(Roles = "admin")]
    public IActionResult AddArticleGet()
    {
        return View("AddArticle");
    }

    [HttpPost("/AddArticle")]
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