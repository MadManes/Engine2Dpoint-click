namespace Engine.Math;

public class LineSegment
{
    public Vector2 Start { get; }

    public Vector2 End { get; }

    public LineSegment(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }

    public float Length()
    {
        return (End - Start).Length();
    }

    public Vector2 Direction()
    {
        return (End - Start).Normalize();
    }

    public Vector2 MidPoint()
    {
        return new Vector2(
            (Start.X + End.X) / 2f,
            (Start.Y + End.Y) / 2f
        );
    }

    
    public Vector2 ClosestPoint(Vector2 point)
    {
        Vector2 ab = End - Start;
        Vector2 ap = point - Start;

        float denominator = ab.Dot(ab);

        if (denominator == 0)
            return Start;

        float t = ap.Dot(ab) / denominator;

        t = MathHelper.Clamp(t, 0f, 1f);

        return Start + ab * t;
    }
}