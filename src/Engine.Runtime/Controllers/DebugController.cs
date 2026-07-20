using Raylib_cs;
using Engine.Runtime.Debug;
using Engine.Runtime.Rendering;

namespace Engine.Runtime.Controllers;

public class DebugController
{
    private readonly DebugSettings _settings;
    private readonly Camera _camera;
    private System.Numerics.Vector2 _previousMousePosition;

    public DebugController(DebugSettings settings, Camera camera)
    {
        _settings = settings;
        _camera = camera;
        _previousMousePosition = Raylib.GetMousePosition();
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

        if (Raylib.IsKeyPressed(KeyboardKey.Equal))
        {
            _camera.ZoomIn(0.1f);
        }


        if (Raylib.IsKeyPressed(KeyboardKey.Minus))
        {
            _camera.ZoomOut(0.1f);
        }


        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            _camera.Reset();
        }

        System.Numerics.Vector2 currentMouse = Raylib.GetMousePosition();


        if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            System.Numerics.Vector2 delta =
                currentMouse - _previousMousePosition;


            _camera.Pan(
                -delta.X,
                -delta.Y
            );
        }


        _previousMousePosition = currentMouse;
    }
}