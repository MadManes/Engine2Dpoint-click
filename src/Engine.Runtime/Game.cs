using Engine.Core;
using Engine.Runtime.Rendering;
using Raylib_cs;

namespace Engine.Runtime;

public class Game : IGameLoop
{
    private readonly DebugRenderer _renderer;

    public Game()
    {
        _renderer = new DebugRenderer();
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

    }

    public void Render()
    {
        _renderer.Render();
    }

    public void Shutdown()
    {
        _renderer.Shutdown();
    }
}