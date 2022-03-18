using System.Text.Json;

namespace homePage2.Models;

public static class JsonOper
{
    private const string jsonPath = @"AppData/appData.json";

    private class Field
    {
        public string? email { get; set; }
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
}