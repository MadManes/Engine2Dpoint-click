using Engine.Math;
using Engine.Entities.Components;

namespace Engine.Scene.Definitions;

public static class WideRoomDefinition
{
    public static SceneData Create()
    {
        Polygon polygon = new(
            new Vector2(100, 100),
            new Vector2(1200, 100),
            new Vector2(1200, 500),
            new Vector2(100, 500)
        );

        NavigationData navigation = new(polygon);

        BackgroundData background = new(
            "wide_room"
        );

        Vector2 playerSpawn = new(
            600,
            300
        );

        EntityCollectionData entityCollection =
        new(
            new List<EntityData>()            
        );

        return new SceneData(
            "Wide Room",
            navigation,
            background,
            entityCollection,
            playerSpawn
        );
    }
}