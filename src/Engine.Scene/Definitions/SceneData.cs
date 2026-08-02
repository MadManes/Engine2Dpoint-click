namespace Engine.Scene.Definitions;

public class SceneData
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";

    public NavigationData Navigation { get; set; } = new();

    public BackgroundData Background { get; set; } = new();

    public EntityCollectionData EntityCollection { get; set; } = new();

    public List<SpawnPointData> SpawnPoints { get; set; } = new();

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