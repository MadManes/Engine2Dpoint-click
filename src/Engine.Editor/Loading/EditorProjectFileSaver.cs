using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Editor.Loading;

public class EditorProjectFileSaver
{
    private readonly string _filePath;
    private readonly ProjectSerializer _serializer;


    public EditorProjectFileSaver(
        string filePath)
    {
        _filePath = filePath;
        _serializer = new ProjectSerializer();
    }


    public void Save(
        ProjectData data)
    {
        string json =
            _serializer.Serialize(
                data
            );

        File.WriteAllText(
            _filePath,
            json
        );
    }
}