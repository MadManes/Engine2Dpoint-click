using Engine.Scene;
using Engine.Scene.Scenes;

namespace Engine.Runtime.SceneTransition;

public class SceneTransitionService
{
    private readonly SceneManager _sceneManager;
    private readonly SceneRegistry _sceneRegistry;

    public SceneTransitionService(
        SceneManager sceneManager,
        SceneRegistry sceneRegistry)
    {
        _sceneManager = sceneManager;
        _sceneRegistry = sceneRegistry;
    }

    public void ChangeScene(
        string sceneName,
        string spawnId)
    {
        GameScene scene =
            _sceneRegistry.Create(
                sceneName
            );
        _sceneManager.LoadScene(
            scene,
            spawnId
        );
    }
}