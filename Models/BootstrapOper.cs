namespace homePage2.Models;

public static class BootstrapOper
{
    public static string Alert(ResultMsg result)
    {
        return @"<div class=""alert alert-" + result.MsgType + @" alert-dismissible"">
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert""></button>"
                + result.MsgText +
                @"</div>";
    }
}