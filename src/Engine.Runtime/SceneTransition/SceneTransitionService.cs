using Engine.Scene;

namespace Engine.Runtime.SceneTransition;

public class SceneTransitionService
{
    private readonly SceneManager _sceneManager;

    public SceneTransitionService(
        SceneManager sceneManager)
    {
        _sceneManager = sceneManager;
    }

    public void ChangeScene(
        GameScene scene,
        string spawnId)
    {
        _sceneManager.LoadScene(
            scene,
            spawnId
        );
    }
}