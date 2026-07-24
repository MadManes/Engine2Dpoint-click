using System.Linq;
using Engine.Scene;
using Engine.Scene.Scenes;
using Engine.Entities;
using Raylib_cs;

namespace Engine.Runtime.Controllers;

public class SceneDebugController
{
    private readonly SceneManager _sceneManager;
    private readonly Entity _player;
    private readonly List<string> _scenes;

    private int _currentIndex;


    public SceneDebugController(
        SceneManager sceneManager,
        Entity player,
        string initialScene)
    {
        _sceneManager = sceneManager;
        _player = player;
        _scenes = SceneCatalog.Names.ToList();
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
            SceneCatalog.Create(
                _scenes[_currentIndex]
            );

        _player.Movement.ClearPath();
        _sceneManager.LoadScene(scene);        
        _player.Transform.SetPosition(
            scene.Data.PlayerSpawn
        );
        scene.World.AddEntity(_player);
    }
}