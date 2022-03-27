namespace homePage2.Models;

public static class AppSettingsOper
{
    public static string GetHostPath(IConfiguration config)
    {
        return config.GetValue<string>("HostPath");
    }
}