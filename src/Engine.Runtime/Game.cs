using Engine.Core;
using Engine.Scene.World;
using Engine.Runtime.Rendering;
using Engine.Entities;
using Engine.Math;
using Engine.Runtime.Debug;
using Raylib_cs;

namespace Engine.Runtime;

public class Game : IGameLoop
{
    private readonly IRenderer _renderer;
    private readonly World _world;
    private readonly Entity _player;
    private Vector2? _mousePosition;
    private Vector2? _correctedPosition;

    public Game()
    {
        _renderer = new DebugRenderer();
        _world = DebugWorldFactory.CreateTestWorld();
        _player = new Entity("Guybrush");

        _player.Transform.SetPosition(
            new Vector2(200, 100)
        );
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

        if (_player.Movement.Destination.HasValue)
        {
            Vector2 currentPosition = _player.Transform.Position;
            Vector2 destination = _player.Movement.Destination.Value;

            float distance = currentPosition.Distance(destination);

            if (distance > 1f)
            {
                Vector2 direction =
                    (destination - currentPosition).Normalize();

                Vector2 displacement =
                    direction *
                    _player.Movement.Speed *
                    deltaTime;

                _player.Transform.Translate(displacement);
            }
        }
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