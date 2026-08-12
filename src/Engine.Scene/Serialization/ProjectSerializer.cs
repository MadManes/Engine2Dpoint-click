using System.Text.Json;
using Engine.Scene.Definitions;

namespace Engine.Scene.Serialization;

public class ProjectSerializer
{
    public ProjectData Deserialize(string json)
    {
        ProjectData? data =
            JsonSerializer.Deserialize<ProjectData>(
                json,
                JsonOptions.Default
            );

        if(data == null)
        {
            throw new InvalidOperationException(
                "Invalid project data."
            );
        }

        return data;
    }

    public string Serialize(ProjectData data)
    {
        return JsonSerializer.Serialize(
            data,
            JsonOptions.Default
        );
    }
}