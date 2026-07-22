using Engine.Core;
using Engine.Math;

namespace Engine.Navigation;

public class PathBuilder
{
    private readonly NavigationPath _path;


    public PathBuilder()
    {
        _path = new NavigationPath();
    }


    public PathBuilder AddPoint(Vector2 point)
    {
        _path.AddPoint(point);

        return this;
    }


    public NavigationPath Build()
    {
        return _path;
    }
}