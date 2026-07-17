namespace Engine.Scene;

public class SceneManager
{
    public IScene? CurrentScene { get; private set; }

    public void LoadScene(IScene scene)
    {
        CurrentScene = scene;
    }
}