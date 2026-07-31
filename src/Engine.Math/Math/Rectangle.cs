namespace Engine.Math;

public class Rectangle
{
    public Vector2 Position { get; }
    public float Width { get; }
    public float Height { get; }

    public Rectangle(Vector2 position,
        float width,
        float height)
    {
        Position = position;
        Width = width;
        Height = height;
    }

    public bool Contains(Vector2 point)
    {
        return point.X >= Position.X &&
               point.X <= Position.X + Width &&
               point.Y >= Position.Y &&
               point.Y <= Position.Y + Height;
    }
}
