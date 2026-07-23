using Engine.Math;
using Engine.Scene.World;

namespace Engine.Scene.Scenes;

public class TestRoomScene : GameScene
{
    public TestRoomScene()
        : base(
            "Test Room",
            CreateWorld()
        )
    {        
    }

    private static SceneWorld CreateWorld()
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

        return new SceneWorld(walkableArea);
    }
}