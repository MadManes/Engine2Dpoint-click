using Engine.Math;

namespace Engine.Scene.Definitions;

public class SpawnPointData
{
    public string Id { get; }
    public Vector2 Position { get; }

    public SpawnPointData(
        string id,
        Vector2 position)
    {
        Id = id;
        Position = position;
    }
}