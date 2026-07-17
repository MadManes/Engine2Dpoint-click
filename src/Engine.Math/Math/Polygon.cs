namespace Engine.Math;

public class Polygon
{
    public Polygon(params Vector2[] vertices)
    {
        _vertices.AddRange(vertices);
        _edges = new();
        BuildEdges();
    }

    public Polygon()
    {
    }

    private readonly List<Vector2> _vertices = new();
    private readonly List<LineSegment> _edges = new();

    public IReadOnlyList<Vector2> Vertices => _vertices;

    public Vector2 GetClosestPoint(Vector2 point)
    {
        Vector2 closest = _vertices[0];

        float minimumDistance = float.MaxValue;

        foreach (LineSegment edge in _edges)
        {
            Vector2 candidate =
                edge.ClosestPoint(point);


            float distance =
                point.Distance(candidate);


            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                closest = candidate;
            }
        }

        return closest;
    }

    public bool Contains(Vector2 point)
    {
        bool inside = false;
        int count = _vertices.Count;

        for (int i = 0, j = count - 1; i < count; j = i++)
        {
            Vector2 a = _vertices[i];
            Vector2 b = _vertices[j];

            bool intersects =
                ((a.Y > point.Y) != (b.Y > point.Y)) &&
                (point.X < (b.X - a.X) * (point.Y - a.Y) / (b.Y - a.Y) + a.X);

            if (intersects)
            {
                inside = !inside;
            }
        }

        return inside;
    }
    
    public void AddVertex(Vector2 vertex)
    {
        _vertices.Add(vertex);
    }

    private void BuildEdges()
    {
        _edges.Clear();

        for (int i = 0; i < _vertices.Count; i++)
        {
            Vector2 start = _vertices[i];
            Vector2 end = _vertices[(i + 1) % _vertices.Count];

            LineSegment edge = new(start, end);
            _edges.Add(edge);
        }
    }
}