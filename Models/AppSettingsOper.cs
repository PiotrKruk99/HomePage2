namespace homePage2.Models;

public class AppSettingsOper
{
    private IConfiguration _config;

    public AppSettingsOper(IConfiguration config)
    {
        _config = config;
    }

    public string GetHostPath()
    {
        return _config.GetValue<string>("HostPath");
    }
}