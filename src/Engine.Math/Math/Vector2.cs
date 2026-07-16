namespace Engine.Math;

public struct Vector2
{
    public float X;

    public float Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 Zero => new(0f, 0f);

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}