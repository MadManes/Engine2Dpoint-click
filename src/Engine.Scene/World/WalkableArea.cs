using Engine.Math;
namespace Engine.Scene.World;


public class WalkableArea : IWalkableArea
{
    public NavigationType NavigationType { get; }

    public Polygon Polygon { get; }

    public WalkableArea(NavigationType navigationType, Polygon polygon)
    {
        NavigationType = navigationType;
        Polygon = polygon;
    }

    public bool Contains(Vector2 position)
    {
        return Polygon.Contains(position);
    }

    public Vector2 GetClosestPoint(Vector2 position)
    {
        return Polygon.GetClosestPoint(position);
    }
}