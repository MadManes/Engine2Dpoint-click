using Raylib_cs;
using Engine.Runtime.Debug;

namespace Engine.Runtime.Controllers;

public class DebugController
{
    private readonly DebugSettings _settings;

    public DebugController(DebugSettings settings)
    {
        _settings = settings;
    }


    public void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.F1))
        {
            _settings.ToggleWorld();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.F2))
        {
            _settings.TogglePlayer();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.F3))
        {
            _settings.ToggleMouse();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.F4))
        {
            _settings.ToggleCorrectedPoint();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.F5))
        {
            _settings.ToggleNavigationCorrection();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.F6))
        {
            _settings.ToggleFPS();
        }
    }
}