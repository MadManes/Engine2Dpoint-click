using Raylib_cs;
using System.Numerics;

namespace Engine.Runtime.Rendering;

public class Camera
{
    private Camera2D _camera;


    public Camera()
    {
        _camera = new Camera2D
        {
            Target = Vector2.Zero,
            Offset = Vector2.Zero,
            Rotation = 0f,
            Zoom = 1f
        };
    }


    public Camera2D NativeCamera => _camera;



    public void SetPosition(float x, float y)
    {
        _camera.Target = new Vector2(x, y);
    }



    public void SetZoom(float zoom)
    {
        _camera.Zoom = zoom;
    }  


    public Engine.Math.Vector2 ScreenToWorld(
    float x,
    float y)
    {
        Vector2 screenPosition =
            new(x, y);


        Vector2 worldPosition =
            Raylib.GetScreenToWorld2D(
                screenPosition,
                _camera
            );


        return new Engine.Math.Vector2(
            worldPosition.X,
            worldPosition.Y
        );
    }

    public void Reset()
    {
        _camera.Target = Vector2.Zero;
        _camera.Zoom = 1f;
    }


    public void ZoomIn(float amount)
    {
        _camera.Zoom += amount;
    }


    public void ZoomOut(float amount)
    {
        _camera.Zoom -= amount;

        if (_camera.Zoom < 0.1f)
        {
            _camera.Zoom = 0.1f;
        }
    }

    public void Pan(float x, float y)
    {
        _camera.Target += new Vector2(x, y);
    }
}