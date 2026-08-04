using Engine.Scene.Definitions;

namespace Engine.Scene.Loading;

public class SceneRepository
{
    private readonly SceneProject _project;


    public SceneRepository(
        SceneProject project)
    {
        _project = project;
    }


    public IReadOnlyCollection<string> SceneIds =>
        _project.SceneIds;


    public SceneData Get(string id)
    {
        return _project.GetScene(id);
    }
}