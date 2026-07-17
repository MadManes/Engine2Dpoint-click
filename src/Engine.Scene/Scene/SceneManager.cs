namespace Engine.Scene.World;

public class SceneManager
{
    public IScene? CurrentScene { get; private set; }

    public void LoadScene(IScene scene)
    {
        CurrentScene = scene;
    }
}