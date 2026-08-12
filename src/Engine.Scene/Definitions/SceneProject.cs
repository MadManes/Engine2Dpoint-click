namespace Engine.Scene.Definitions;

public class SceneProject
{
    public IReadOnlyList<SceneData> Scenes { get; }
    public string StartupSceneId { get; }


    public IReadOnlyCollection<string> SceneIds =>
        Scenes
            .Select(x => x.Id)
            .ToList();


    public SceneProject(
        IReadOnlyList<SceneData> scenes,
        string startupSceneId)
    {
        Scenes = scenes;
        StartupSceneId = startupSceneId;
    }


    public SceneData GetScene(string id)
    {
        SceneData? scene =
            Scenes.FirstOrDefault(
                x => x.Id == id
            );


        if (scene == null)
        {
            throw new InvalidOperationException(
                $"Scene '{id}' does not exist."
            );
        }


        return scene;
    }
}