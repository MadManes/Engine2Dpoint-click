using Engine.Math;

namespace Engine.Core;

public class NavigationPath
{
    private readonly List<Vector2> _points = new();

    private int _currentIndex;

    public IReadOnlyList<Vector2> Points => _points;

    public bool IsEmpty => _points.Count == 0;

    public int CurrentIndex => _currentIndex;
    public bool HasCurrentPoint => _currentIndex < _points.Count;
    public Vector2 CurrentPoint => _points[_currentIndex];
    

    public void AddPoint(Vector2 point)
    {
        _points.Add(point);
    }

    public bool MoveNext()
    {
        _currentIndex++;

        return _currentIndex < _points.Count;
    }

    public void Clear()
    {
        _points.Clear();

        _currentIndex = 0;
    }
}