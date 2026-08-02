using Engine.Scene.Loading;
using Engine.Scene.Definitions;

namespace Engine.Scene.Scenes;

public class SceneRegistry
{
    private readonly SceneLoader _loader;

    public SceneRegistry(
        SceneLoader loader)
    {
        _loader = loader;
    }

    public GameScene Create(
        string name)
    {
        SceneData data = _loader.Load(name);
        
        return new GameScene(data);
    }
}