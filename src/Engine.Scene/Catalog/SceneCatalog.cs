namespace Engine.Scene.Catalog;

using Engine.Scene.Definitions;


public class SceneCatalog
{
    private readonly Dictionary<string, SceneData> _scenes;


    public SceneCatalog(
        IEnumerable<SceneData> scenes)
    {
        _scenes =
            scenes.ToDictionary(
                x => x.Id
            );
    }


    public IReadOnlyCollection<string> Ids =>
        _scenes.Keys;


    public SceneData Get(string id)
    {
        if (!_scenes.TryGetValue(
            id,
            out SceneData? scene))
        {
            throw new KeyNotFoundException(
                $"Scene '{id}' not found."
            );
        }

        return scene;
    }
}