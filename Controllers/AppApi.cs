using Microsoft.AspNetCore.Mvc;
using homePage2.Models;

namespace homePage2.Controllers;

[ApiController]
//[Route("api/[controller]")]
//[Route("GetFormatedText")]
public class AppApi : ControllerBase
{
    [HttpGet("GetFormatedText")]
    public string Get(string content)
    {
        Article article = new Article() {Content = content};
        article = TagsOper.RemoveTags(article);
        article = TagsOper.AddTags(article);

        return article.Content;
    }
}