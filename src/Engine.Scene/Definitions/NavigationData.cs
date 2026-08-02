using Engine.Math;

namespace Engine.Scene.Definitions;

public class NavigationData
{
    public List<Vector2> Vertices { get; set; } = new();
    //public Polygon WalkablePolygon { get; }

    //public NavigationData(
    //    Polygon walkablePolygon)
    //{
    //    WalkablePolygon = walkablePolygon;
    //}
}