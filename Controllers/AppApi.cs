using Microsoft.AspNetCore.Mvc;

namespace homePage2.Controllers;

[ApiController]
//[Route("api/[controller]")]
//[Route("GetFormatedText")]
public class AppApi : ControllerBase
{
    [HttpGet("GetFormatedText")]
    public string Get()
    {
        return "test";
    }
}