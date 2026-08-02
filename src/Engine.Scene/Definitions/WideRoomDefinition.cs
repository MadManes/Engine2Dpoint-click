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

        List<SpawnPointData>spawnPoints =
        [
            new SpawnPointData(
                "Default",
                new Vector2(600, 300)
            ),
            new SpawnPointData(
                "Entrance",
                new Vector2(150, 250)
            )
        ];

        EntityCollectionData entityCollection =
        new(
            new List<EntityData>
            {
                new EntityData(
                    "door_test_room",
                    "to Test Room",
                    new Vector2(150,200),
                    new SpriteData("door"),
                    EntityType.Generic,
                    true,
                    new DoorData(
                        "TestRoom",
                        "Entrance"
                    )
                )
            }                      
        );

        

        return new SceneData(
            "Wide Room",
            navigation,
            background,
            entityCollection,
            spawnPoints
        );
    }
}