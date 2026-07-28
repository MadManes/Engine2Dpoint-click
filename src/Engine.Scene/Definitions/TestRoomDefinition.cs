using Engine.Math;
using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Scene.Definitions;

public static class TestRoomDefinition
{
    public static SceneData Create()
    {
        Polygon polygon = new(
            new Vector2(240, 10),
            new Vector2(1100, 10),
            new Vector2(1100, 600),
            new Vector2(240, 600)
        );

        NavigationData navigation = new(polygon);

        BackgroundData background = new(
            "test_room"
        );

        Vector2 playerSpawn = new(
            600,
            100
        );
        
        EntityCollectionData entityCollection =
        new(
            new List<EntityData>()
        );

        return new SceneData(
            "Test Room",
            navigation,
            background,
            entityCollection,
            playerSpawn
        );
    }
}