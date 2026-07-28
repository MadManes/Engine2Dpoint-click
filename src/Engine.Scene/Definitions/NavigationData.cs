using Engine.Math;

namespace Engine.Scene.Definitions;

public class NavigationData
{
    public Polygon WalkablePolygon { get; }

    public NavigationData(
        Polygon walkablePolygon)
    {
        WalkablePolygon = walkablePolygon;
    }
}