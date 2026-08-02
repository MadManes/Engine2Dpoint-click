using System.Text.Json;
using System.Text.Json.Serialization;

namespace Engine.Scene.Serialization;

public static class JsonOptions
{
    public static JsonSerializerOptions Default =>
        new()
        {
            WriteIndented = true,
    
            PropertyNamingPolicy =
                JsonNamingPolicy.CamelCase,
    
            PropertyNameCaseInsensitive = true,
    
            DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull,
    
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
}