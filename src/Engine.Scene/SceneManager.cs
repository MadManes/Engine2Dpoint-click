namespace Engine.Scene;

public class SceneManager
{
    public GameScene? CurrentScene { get; private set; }

    public void LoadScene(GameScene scene)
    {
        CurrentScene = scene;
    }
}