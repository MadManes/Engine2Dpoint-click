using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Scene.Loading.Project;

public class ProjectLoader
{
    private readonly string _filePath;
    private readonly ProjectSerializer _serializer;

    public ProjectLoader(
        string filePath,
        ProjectSerializer serializer)
    {
        _filePath = filePath;
        _serializer = serializer;
    }

    public ProjectData Load()
    {
        string json =
            File.ReadAllText(
                _filePath
            );

        return _serializer.Deserialize(
            json
        );
    }
}