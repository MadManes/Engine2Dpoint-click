using Engine.Math;
using Engine.Editor.Camera;
using Raylib_cs;

namespace Engine.Editor.Input;

public class EditorInputController
{
    private readonly EditorCamera _camera;

    private bool _previousLeftDown;


    public EditorInputController(
        EditorCamera camera)
    {
        _camera = camera;
    }


    public Vector2 MouseWorldPosition()
    {
        System.Numerics.Vector2 mouse =
            Raylib.GetMousePosition();


        System.Numerics.Vector2 world =
            Raylib.GetScreenToWorld2D(
                mouse,
                _camera.NativeCamera
            );


        return new Vector2(
            world.X,
            world.Y
        );
    }


    public bool LeftDown()
    {
        return Raylib.IsMouseButtonDown(
            MouseButton.Left
        );
    }


    public bool LeftPressed()
    {
        bool current =
            Raylib.IsMouseButtonDown(
                MouseButton.Left
            );


        return current &&
               !_previousLeftDown;
    }

    public bool RightPressed()
    {
        return Raylib.IsMouseButtonPressed(
            MouseButton.Right
        );
    }


    public bool LeftReleased()
    {
        bool current =
            Raylib.IsMouseButtonDown(
                MouseButton.Left
            );


        return !current &&
               _previousLeftDown;
    }


    public void EndFrame()
    {
        _previousLeftDown =
            Raylib.IsMouseButtonDown(
                MouseButton.Left
            );
    }
}