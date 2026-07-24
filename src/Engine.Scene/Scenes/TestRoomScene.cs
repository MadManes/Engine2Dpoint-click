using Engine.Scene.Definitions;
using Engine.Scene.World;

namespace Engine.Scene.Scenes;

public class TestRoomScene : GameScene
{
    public TestRoomScene()
        : base(
            TestRoomDefinition.Create()
        )
    {
    }
}