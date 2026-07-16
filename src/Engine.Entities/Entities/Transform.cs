using Engine.Math;

namespace Engine.Entities;

public class Transform
{
    public Vector2 Position { get; set; }

    public float Z { get; set; }

    public float Scale { get; set; }

    public Transform()
    {
        Position = Vector2.Zero;
        Z = 0f;
        Scale = 1f;
    }
}