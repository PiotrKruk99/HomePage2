using System.Text.Json;

namespace homePage2.Models;

public static class JsonOper
{
    private const string jsonPath = @"AppData/appData.json";

    public class Field
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
            var field = JsonSerializer.Deserialize<Field>(File.ReadAllText(jsonPath));
            return field == null ? null : field.email;
        }
        catch (Exception)
        {
            return null;
        }

        // Utf8JsonReader reader = new Utf8JsonReader(File.ReadAllBytes(jsonPath));

        // while (reader.Read())
        // {
        //     if (reader.TokenType == JsonTokenType.PropertyName
        //     && reader.GetString() != null && reader.GetString() == property
        //     && reader.Read() && reader.TokenType == JsonTokenType.String)
        //     {
        //         return reader.GetString();
        //     }
        // }

        // return null;
    }

    public static Field? ReadField()
    /*get all email data*/
    {
        if (!File.Exists(jsonPath))
        {
            return null;
        }

        try
        {
            var field = JsonSerializer.Deserialize<Field>(File.ReadAllText(jsonPath));

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