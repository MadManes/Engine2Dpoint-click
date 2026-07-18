using Engine.Math;
using Engine.Scene.World;

namespace Engine.Runtime.Debug;

public static class DebugWorldFactory
{
    public static World CreateTestWorld()
    {
        Polygon polygon = new(
            new Vector2(2, 2),
            new Vector2(800, 2),
            new Vector2(800, 600),
            new Vector2(2, 600)
        );

        WalkableArea walkableArea = new(
            NavigationType.Polygon,
            polygon
        );

        return new World(walkableArea);
    }
}