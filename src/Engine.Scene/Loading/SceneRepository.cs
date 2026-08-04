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
        _project.Scenes
            .Select(x => x.Id)
            .ToList();


    public SceneData Get(string id)
    {
        SceneData? scene =
            _project.Scenes
                .FirstOrDefault(
                    x => x.Id == id
                );


        if (scene == null)
        {
            throw new FileNotFoundException(
                $"Scene '{id}' not found."
            );
        }


        return scene;
    }
}