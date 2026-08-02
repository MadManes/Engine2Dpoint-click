namespace Engine.Scene.Definitions;

public class SceneData
{
    public string Name { get; }

    public NavigationData Navigation { get; }

    public BackgroundData Background { get; }

    public EntityCollectionData EntityCollection { get; }

    public IReadOnlyList<SpawnPointData> SpawnPoints { get; }

    public SceneData(
        string name,
        NavigationData navigation,
        BackgroundData background,
        EntityCollectionData entityCollection,
        IReadOnlyList<SpawnPointData> spawnPoints)
    {
        Name = name;
        Navigation = navigation;
        Background = background;
        EntityCollection = entityCollection;
        SpawnPoints = spawnPoints;
    }

    public SpawnPointData GetSpawnPoint(string id)
    {
        SpawnPointData? spawn =
            SpawnPoints.FirstOrDefault(
                x => x.Id == id
            );
    
        if (spawn == null)
        {
            throw new InvalidOperationException(
                $"Spawn point '{id}' does not exist in scene '{Name}'."
            );
        }
    
        return spawn;
    }
}