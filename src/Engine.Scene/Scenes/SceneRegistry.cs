namespace Engine.Scene.Scenes;

public static class SceneRegistry
{
    public static GameScene Create(string name)
    {
        return name switch
        {
            "TestRoom" => new TestRoomScene(),
            "WideRoom" => new WideRoomScene(),

            _ => throw new InvalidOperationException(
                $"Scene '{name}' does not exist."
            )
        };
    }
}