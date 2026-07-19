using Engine.Math;
using Engine.Scene.World;

namespace Engine.Runtime.Debug;

public static class DebugWorldFactory
{
    public static World CreateTestWorld()
    {
        Polygon polygon = new(
            new Vector2(240, 10),
            new Vector2(1100, 10),
            new Vector2(1100, 600),
            new Vector2(240, 600)
        );

        WalkableArea walkableArea = new(
            NavigationType.Polygon,
            polygon
        );

        return new World(walkableArea);
    }
}