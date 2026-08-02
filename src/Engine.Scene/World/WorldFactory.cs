using Engine.Scene.Definitions;
using Engine.Math;

namespace Engine.Scene.World;

public static class WorldFactory
{
    public static SceneWorld Create(SceneData data)
    {
        Polygon polygon =
            new(
                data.Navigation.Vertices.ToArray()
            );

        WalkableArea area = new(
            NavigationType.Polygon,
            polygon
        );

        return new SceneWorld(area);
    }
}