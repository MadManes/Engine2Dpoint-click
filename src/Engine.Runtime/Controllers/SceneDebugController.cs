using Engine.Scene;
using Engine.Scene.Loading;
using Engine.Scene.Scenes;
using Raylib_cs;

namespace Engine.Runtime.Controllers;

public class SceneDebugController
{
    private readonly SceneManager _sceneManager;    
    private readonly List<string> _scenes;
    private readonly SceneRegistry _registry;
    private readonly SceneLoader _loader;

    public event Action<GameScene>? SceneChanged;
    private int _currentIndex;


    public SceneDebugController(
        SceneManager sceneManager,
        SceneRegistry registry,
        SceneLoader loader,        
        string initialScene)
    {
        _sceneManager = sceneManager;
        _registry = registry;
        _loader = loader;

        _scenes = _loader.Names.ToList();
        _currentIndex = _scenes.IndexOf(            
            initialScene
        );

        if (_currentIndex < 0)
        {
            _currentIndex = 0;
        }
    }


    public void Update()
    {
        if (Raylib.IsKeyPressed(
            KeyboardKey.F7))
        {
            NextScene();
        }
    }


    private void NextScene()
    {
        _currentIndex++;

        if (_currentIndex >= _scenes.Count)
        {
            _currentIndex = 0;
        }


        GameScene scene =
            _registry.Create(
                _scenes[_currentIndex]
            );
        
        _sceneManager.LoadScene(scene);
        SceneChanged?.Invoke(scene);

    }
}