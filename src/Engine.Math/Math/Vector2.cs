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

    public float Length()
    {
        return MathF.Sqrt(
            X * X + 
            Y * Y
        );
    }

    public Vector2 Normalize()
    {
        float length = Length();
    
        if(length == 0)
            return Zero;
    
    
        return new Vector2(
            X / length,
            Y / length
        );
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X + b.X,
            a.Y + b.Y
        );
    }



    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(
            a.X - b.X,
            a.Y - b.Y
        );
    }



    public static Vector2 operator *(Vector2 vector, float scalar)
    {
        return new Vector2(
            vector.X * scalar,
            vector.Y * scalar
        );
    }



    public static Vector2 operator /(Vector2 vector, float scalar)
    {
        return new Vector2(
            vector.X / scalar,
            vector.Y / scalar
        );
    }

    public float Dot(Vector2 other)
    {
        return X * other.X + Y * other.Y;
    }

    public float Distance(Vector2 other)
    {
        return (this - other).Length();
    }
    
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}