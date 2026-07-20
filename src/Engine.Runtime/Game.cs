using Engine.Core;
using Engine.Scene.World;
using Engine.Runtime.Systems;
using Engine.Runtime.Controllers;
using Engine.Runtime.Rendering;
using Engine.Runtime.Debug;
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
    private readonly World _world;
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
        _navigationController = new NavigationController();        
        _world = DebugWorldFactory.CreateTestWorld();        
        _player = new Entity("Guybrush");

        _player.Transform.SetPosition(
            new Vector2(600, 100)
        );

        _world.AddEntity(_player);
        
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
        
            _correctedPosition = _navigationController.GetDestination(
                _world,
                clickPosition
            );

            _player.Movement.SetDestination(_correctedPosition.Value);
        }

        _movementSystem.Update(_world, deltaTime);
        
    }

    public void Render()
    {
        RenderContext context = new(
            _world,
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