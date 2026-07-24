using Engine.Math;

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


        Vector2 playerSpawn = new(
            600,
            300
        );


        return new SceneData(
            "Wide Room",
            polygon,
            playerSpawn
        );
    }
}