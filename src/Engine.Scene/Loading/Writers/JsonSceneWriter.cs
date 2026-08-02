using System.Text.Json;
using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Scene.Loading.Writers;

public class JsonSceneWriter
{
    private readonly string _folder;


    public JsonSceneWriter(
        string folder)
    {
        _folder = folder;
    }


    public void Save(
        SceneData scene)
    {
        string fileName = GetFileName(scene.Name);

        string path =
            Path.Combine(
                _folder,
                $"{fileName}.json"
            );

        string json =
            JsonSerializer.Serialize(
                scene,
                JsonOptions.Default
            );


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