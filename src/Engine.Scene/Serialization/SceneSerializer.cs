using System.Text.Json;
using Engine.Scene.Definitions;

namespace Engine.Scene.Serialization;

public class SceneSerializer
{
    public SceneData Deserialize(string json)
    {
        SceneData? data =
            JsonSerializer.Deserialize<SceneData>(
                json,
                JsonOptions.Default
            );

        if (data == null)
        {
            throw new InvalidOperationException(
                "Invalid scene data."
            );
        }

        return data;
    }

    public string Serialize(SceneData data)
    {
        return JsonSerializer.Serialize(
            data,
            JsonOptions.Default
        );
    }
}