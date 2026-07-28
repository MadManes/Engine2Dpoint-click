using Engine.Math;

namespace Engine.Scene.Definitions;

public class SceneData
{
    public string Name { get; }

    public NavigationData Navigation { get; }

    public BackgroundData Background { get; }

    public EntityCollectionData EntityCollection { get; }

    public Vector2 PlayerSpawn { get; }

    public SceneData(
        string name,
        NavigationData navigation,
        BackgroundData background,
        EntityCollectionData entityCollection,
        Vector2 playerSpawn)
    {
        Name = name;
        Navigation = navigation;
        Background = background;
        EntityCollection = entityCollection;
        PlayerSpawn = playerSpawn;
    }
}