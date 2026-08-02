using Engine.Scene.Definitions;
using Engine.Scene.Serialization;

namespace Engine.Scene.Loading.Sources;

public class JsonSceneSource : ISceneSource
{
    private readonly string _folder;
    private readonly SceneSerializer _serializer;


    public JsonSceneSource(
        string folder,
        SceneSerializer serializer)
    {
        _folder = folder;
        _serializer = serializer;
    }


    public IReadOnlyCollection<string> Names
    {
        get
        {
            List<string> ids = new();

            foreach (string file in Directory.GetFiles(_folder, "*.json"))
            {
                string json =
                    File.ReadAllText(file);

                SceneData data =
                    _serializer.Deserialize(json);

                ids.Add(data.Id);
            }

            return ids;
        }
    }


    public SceneData Load(string id)
    {
        foreach (string file in Directory.GetFiles(_folder, "*.json"))
        {
            string json =
                File.ReadAllText(file);

            SceneData data =
                _serializer.Deserialize(json);

            if (data.Id == id)
            {
                return data;
            }
        }

        throw new FileNotFoundException(
            $"Scene '{id}' not found."
        );
    }
}