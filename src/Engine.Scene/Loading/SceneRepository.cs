using Engine.Scene.Definitions;
using Engine.Scene.Catalog;

namespace Engine.Scene.Loading;

public class SceneRepository
{
    private readonly SceneCatalog _catalog;


    public SceneRepository(
        SceneCatalog catalog)
    {
        _catalog = catalog;
    }


    public IReadOnlyCollection<string> SceneIds =>
        _catalog.Ids;


    public SceneData Get(string id)
    {
        return _catalog.Get(id);
    }
}