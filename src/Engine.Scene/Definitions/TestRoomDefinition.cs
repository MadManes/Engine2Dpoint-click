using Engine.Math;

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


        Vector2 playerSpawn = new(
            600,
            100
        );


        return new SceneData(
            "Test Room",
            polygon,
            playerSpawn
        );
    }
}