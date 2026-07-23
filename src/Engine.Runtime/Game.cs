using Engine.Core;
using Engine.Scene;
using Engine.Scene.Scenes;
using Engine.Scene.World;
using Engine.Runtime.Systems;
using Engine.Runtime.Controllers;
using Engine.Runtime.Rendering;
using Engine.Runtime.Debug;
using Engine.Navigation;
using Engine.Entities;
using Engine.Math;
using Raylib_cs;

namespace Engine.Runtime;

public class Game : IGameLoop
{
    private readonly IRenderer _renderer;
    private readonly MovementSystem _movementSystem;
    private readonly NavigationController _navigationController;
    private readonly DebugController _debugController;
    private readonly DebugSettings _debugSettings;
    private readonly SceneManager _sceneManager;
    private readonly Entity _player;
    private readonly Camera _camera;    
    
    
    private Vector2? _mousePosition;
    private Vector2? _correctedPosition;
    
    public Game()
    {
        _debugSettings = new DebugSettings();
        _camera = new Camera();
        _camera.SetZoom(1.5f);
        _debugController = new DebugController(_debugSettings, _camera); 
        _renderer = new DebugRenderer(_debugSettings, _camera);
        _movementSystem = new MovementSystem();
        _navigationController = new NavigationController(
            new ZigZagPathFinder()
        );
        _sceneManager = new SceneManager();
        _sceneManager.LoadScene(
            new TestRoomScene()
        );
        _player = new Entity("Guybrush");

        _player.Transform.SetPosition(
            new Vector2(600, 100)
        );

        _sceneManager.CurrentScene!.World.AddEntity(_player);
        
    }

    public void Run()
    {
        Initialize();

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Time.DeltaTime();

            Update(deltaTime);
            Render();
        }

        Shutdown();
    }

    public void Initialize()
    {
        _renderer.Initialize();
    }

    public void Update(float deltaTime)    
    {
        _debugController.Update();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            System.Numerics.Vector2 mouse = Raylib.GetMousePosition();

            Vector2 clickPosition = _camera.ScreenToWorld(
                mouse.X,
                mouse.Y
            );

            _mousePosition = clickPosition;            
        
            NavigationPath path = _navigationController.GetPath(
                _sceneManager.CurrentScene!.World,
                _player.Transform.Position,
                clickPosition
            );

    
            if (!path.IsEmpty)
            {
                Vector2 destination = path.Points[0];
                _correctedPosition = destination;                
                _player.Movement.SetPath(path);
            }
        }

        _movementSystem.Update(_sceneManager.CurrentScene!.World, deltaTime);
        
    }

    public void Render()
    {
        RenderContext context = new(
            _sceneManager.CurrentScene!,
            _player,
            _mousePosition,
            _correctedPosition            
        );
        _renderer.Render(context);
    }

    public void Shutdown()
    {
        _renderer.Shutdown();
    }
}