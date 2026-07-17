using Engine.Math;

namespace Engine.Entities;

public class Transform : Component
{
    public Vector2 Position { get; private set; }

    public float Z { get; private set; }

    public float Scale { get; private set; }

    public Transform()
    {
        Position = Vector2.Zero;
        Z = 0f;
        Scale = 1f;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void Translate(Vector2 offset)
    {
        Position += offset;
    }

    public void SetScale(float scale)
    {
        Scale = scale;
    }

    public void SetDepth(float z)
    {
        Z = z;
    }
}