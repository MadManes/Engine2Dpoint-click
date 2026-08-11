using Engine.Editor.Documents;
using Engine.Scene.Serialization;

namespace Engine.Editor.Loading;

public class EditorSceneSaver
{
    private readonly string _sceneFolder;
    private readonly SceneSerializer _serializer;

    public EditorSceneSaver(
        string sceneFolder)
    {
        _sceneFolder = sceneFolder;
        _serializer = new SceneSerializer();
    }


    public void Save(
        SceneDocument document)
    {
        string path =
            Path.Combine(
                _sceneFolder,
                document.FileName
            );

        string json =
            _serializer.Serialize(
                document.Scene
            );

        File.WriteAllText(
            path,
            json
        );
    }
}