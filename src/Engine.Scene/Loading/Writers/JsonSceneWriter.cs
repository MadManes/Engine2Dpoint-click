using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Scene.Loading.Writers;

public class JsonSceneWriter
{
    private readonly string _folder;
    private readonly SceneSerializer _serializer;

    public JsonSceneWriter(
        string folder,
        SceneSerializer serializer)
    {
        _folder = folder;
        _serializer = serializer;
    }


    public void Save(SceneData scene)
    {
        string path =
            Path.Combine(
                _folder,
                $"{scene.Id}.json"
            );

        string json =
            _serializer.Serialize(scene);

        File.WriteAllText(
            path,
            json
        );
    }

    private string GetFileName(string sceneName)
    {
        return sceneName
            .Trim()
            .Replace(" ", "");            
    }
}