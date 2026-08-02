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

        List<SpawnPointData>spawnPoints =
        [
            new SpawnPointData(
                "Default",
                new Vector2(600, 100)
            ),
            new SpawnPointData(
                "Entrance",
                new Vector2(850, 250)
            )
        ];

        EntityCollectionData entityCollection =
        new(
            new List<EntityData>
            {
                new EntityData(
                    "object_1",
                    "NPC_Test_1",
                    new Vector2(600,200),
                    new SpriteData("npc_test"),
                    EntityType.Generic,
                    true
                ),

                new EntityData(
                    "object_2",
                    "NPC_Test_2",
                    new Vector2(350,250),
                    new SpriteData("npc_test"),
                    EntityType.Generic                    
                ),

                new EntityData(
                    "door_wide_room",
                    "to Wide Room",
                    new Vector2(850,200),
                    new SpriteData("door"),
                    EntityType.Generic,
                    true,
                    new DoorData(
                        "WideRoom",
                        "Entrance"
                    )
                ),

            }
        );        

        return new SceneData(
            "Test Room",
            navigation,
            background,
            entityCollection,
            spawnPoints
        );
    }
}