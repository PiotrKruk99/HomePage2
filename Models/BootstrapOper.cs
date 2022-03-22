namespace homePage2.Models;

public static class BootstrapOper
{
    public static string BootstrapAlert(ResultMsg result)
    {
        return @"<div class=""alert alert-" + result.MsgType + @" alert-dismissible"">
                <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert""></button>"
                + result.MsgText +
                @"</div>";
    }
}