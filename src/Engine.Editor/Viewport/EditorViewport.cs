using Engine.Core.Display;

namespace Engine.Editor.Viewport;

public class EditorViewport
{
    private DisplayViewport _viewport;


    public EditorViewport()
    {
        _viewport =
            new DisplayViewport(
                0,
                0,
                0,
                0,
                1
            );
    }


    public int X =>
        _viewport.X;


    public int Y =>
        _viewport.Y;


    public int Width =>
        _viewport.Width;


    public int Height =>
        _viewport.Height;


    public float Scale =>
        _viewport.Scale;


    public void Update(
        DisplayViewport viewport)
    {
        _viewport = viewport;
    }


    public bool Contains(
        int x,
        int y)
    {
        return
            x >= X &&
            x < X + Width &&
            y >= Y &&
            y < Y + Height;
    }


    public bool Contains(
        System.Numerics.Vector2 point)
    {
        return Contains(
            (int)point.X,
            (int)point.Y
        );
    }
}