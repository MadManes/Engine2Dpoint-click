using Engine.Math;
using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class PrimitiveRenderer
{
    public void DrawLine(Vector2 start, Vector2 end, Color color)
    {
        Raylib.DrawLine(
            (int)start.X,
            (int)start.Y,
            (int)end.X,
            (int)end.Y,
            color
        );
    }

    public void DrawCircle(Vector2 center, float radius, Color color)
    {
        Raylib.DrawCircle(
            (int)center.X,
            (int)center.Y,
            radius,
            color
        );
    }
}