using Engine.Core;
using Engine.Scene.World;
using Engine.Runtime.Systems;
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
    private readonly World _world;
    private readonly Entity _player;
    
    private Vector2? _mousePosition;
    private Vector2? _correctedPosition;

    public Game()
    {
        _renderer = new DebugRenderer();
        _movementSystem = new MovementSystem();
        _world = DebugWorldFactory.CreateTestWorld();        
        _player = new Entity("Guybrush");

        _player.Transform.SetPosition(
            new Vector2(200, 100)
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
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            System.Numerics.Vector2 mouse = Raylib.GetMousePosition();

            Vector2 clickPosition = new(
                mouse.X, 
                mouse.Y
            );

            _mousePosition = clickPosition;            
        
            if (_world.WalkableArea.Contains(clickPosition))
            {
                _correctedPosition = clickPosition;
            }
            else
            {
                _correctedPosition =
                    _world.WalkableArea.GetClosestPoint(clickPosition);}
            
            if (_correctedPosition.HasValue)
            {
                _player.Movement.SetDestination(
                    _correctedPosition.Value
                );
            }
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