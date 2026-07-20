using Engine.Math;

namespace Engine.Navigation;

public class NavigationPath
{
    private readonly List<Vector2> _points = new();

    public IReadOnlyList<Vector2> Points => _points;


    public void AddPoint(Vector2 point)
    {
        _points.Add(point);
    }
}