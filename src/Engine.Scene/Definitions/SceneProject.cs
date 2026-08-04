namespace Engine.Scene.Definitions;

public class SceneProject
{
    public IReadOnlyList<SceneData> Scenes { get; }


    public SceneProject(
        IReadOnlyList<SceneData> scenes)
    {
        Scenes = scenes;
    }
}