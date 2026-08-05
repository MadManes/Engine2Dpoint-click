using Engine.Math;
using Engine.Editor.Camera;
using Raylib_cs;

namespace Engine.Editor.Input;

public class EditorInputController
{
    private readonly EditorCamera _camera;


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


    public bool LeftPressed()
    {
        return Raylib.IsMouseButtonPressed(
            MouseButton.Left
        );
    }


    public bool LeftDown()
    {
        return Raylib.IsMouseButtonDown(
            MouseButton.Left
        );
    }


    public bool LeftReleased()
    {
        return Raylib.IsMouseButtonReleased(
            MouseButton.Left
        );
    }
}