using Engine.Scene;

namespace Engine.Scene.Scenes;

public static class SceneCatalog
{
    private static readonly Dictionary<string, Func<GameScene>> _scenes =
        new()
        {
            {
                "TestRoom",
                () => new TestRoomScene()
            },

            {
                "WideRoom",
                () => new WideRoomScene()
            }
        };


    public static IReadOnlyCollection<string> Names =>
        _scenes.Keys;


    public static GameScene Create(string name)
    {
        if (!_scenes.ContainsKey(name))
        {
            throw new InvalidOperationException(
                $"Scene '{name}' does not exist."
            );
        }

        return _scenes[name]();
    }
}