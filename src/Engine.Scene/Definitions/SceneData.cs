using Engine.Math;

namespace Engine.Scene.Definitions;

public class SceneData
{
    public string Name { get; }

    public Polygon WalkablePolygon { get; }

    public Vector2 PlayerSpawn { get; }


    public SceneData(
        string name,
        Polygon walkablePolygon,
        Vector2 playerSpawn)
    {
        Name = name;
        WalkablePolygon = walkablePolygon;
        PlayerSpawn = playerSpawn;
    }
}