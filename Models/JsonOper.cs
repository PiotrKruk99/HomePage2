using System.Text.Json;

namespace homePage2.Models;

public static class JsonOper
{
    private const string jsonPath = @"AppData/appData.json";
    public static string? Read(string property)
    {
        if (!File.Exists(jsonPath))
        {
            return null;
        }

        Utf8JsonReader reader = new Utf8JsonReader(File.ReadAllBytes(jsonPath));

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName
            && reader.GetString() != null && reader.GetString() == property
            && reader.Read() && reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }
        }

        return null;
    }
}