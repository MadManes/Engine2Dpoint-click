using Engine.Scene.Definitions;

namespace Engine.Scene.Loading;

public class SceneRepository
{
    private readonly SceneLoader _loader;


    public SceneRepository(
        SceneLoader loader)
    {
        _loader = loader;
    }


    public IReadOnlyCollection<string> SceneIds =>
        _loader.Names;


    public SceneData Get(string id)
    {
        return _loader.Load(id);
    }
}