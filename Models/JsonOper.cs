using System.Text.Json;

namespace homePage2.Models;

public static class JsonOper
{
    private const string jsonPath = @"AppData/appData.json";

    public class Fields
    {
        public string? email { get; set; }
        public string? mailHost { get; set; }
        public int? mailPort { get; set; }
        public string? mailLogin { get; set; }
        public string? mailPassword { get; set; }
    }

    public static string? ReadEmail()
    /*get email to send admin account link*/
    {
        if (!File.Exists(jsonPath))
        {
            return null;
        }

        try
        {
            var field = JsonSerializer.Deserialize<Fields>(File.ReadAllText(jsonPath));
            return field == null ? null : field.email;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static Fields? ReadFile()
    /*get all email data*/
    {
        if (!File.Exists(jsonPath))
        {
            return null;
        }

        try
        {
            var field = JsonSerializer.Deserialize<Fields>(File.ReadAllText(jsonPath));

            if (field == null || field.email == null || field.mailHost == null
                || field.mailPort == null || field.mailLogin == null
                || field.mailPassword == null)
                return null;
            else
                return field;
        }
        catch (Exception)
        {
            return null;
        }
    }
}