
using System.Text.Json;

namespace ATM.Infrastructure.Utils;
public static class CustomJsonSerializer
{
    private static JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public static T? Deserialize<T>(this string json)
    {       
        return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings);
    }

    public static string Serialize<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, defaultSerializerSettings);
    }
}