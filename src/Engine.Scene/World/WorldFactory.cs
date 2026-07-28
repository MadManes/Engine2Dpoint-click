using Engine.Scene.Definitions;

namespace Engine.Scene.World;

public static class WorldFactory
{
    public static SceneWorld Create(SceneData data)
    {
        WalkableArea area = new(
            NavigationType.Polygon,
            data.Navigation.WalkablePolygon
        );

        return new SceneWorld(area);
    }
}