using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Editor.Loading;

public class EditorProjectFileSaver
{
    private readonly ProjectSerializer _serializer;


    public EditorProjectFileSaver()
    {
        _serializer = new ProjectSerializer();
    }


    public void Save(
        ProjectData data,
        string filePath)
    {
        string json =
            _serializer.Serialize(
                data
            );

        File.WriteAllText(
            filePath,
            json
        );
    }
}