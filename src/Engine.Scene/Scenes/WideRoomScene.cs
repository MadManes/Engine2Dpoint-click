using Engine.Scene.Definitions;

namespace Engine.Scene.Scenes;

public class WideRoomScene : GameScene
{
    public WideRoomScene()
        : base(
            WideRoomDefinition.Create()
        )
    {
    }
}