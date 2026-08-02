using Engine.Scene.Definitions;

namespace Engine.Scene.Loading;

public class SceneLoader
{
    private readonly ISceneSource _source;

    public SceneLoader(
        ISceneSource source)
    {
        _source = source;
    }    

    public IReadOnlyCollection<string> Names => _source.Names;

    public SceneData Load(string name)
    {
        return _source.Load(name);
    }
}