using Raylib_cs;

namespace Engine.Editor.Camera;

public class EditorCamera
{
    private Camera2D _camera;


    public EditorCamera()
    {
        _camera = new Camera2D
        {
            Target = new System.Numerics.Vector2(0,0),
            Offset = new System.Numerics.Vector2(0,0),
            Rotation = 0,
            Zoom = 1
        };
    }


    public Camera2D NativeCamera =>
        _camera;


    public void Update()
    {
        float wheel =
            Raylib.GetMouseWheelMove();


        if (wheel != 0)
        {
            _camera.Zoom += wheel * 0.1f;
        }


        if (_camera.Zoom < 0.1f)
        {
            _camera.Zoom = 0.1f;
        }
    }
}