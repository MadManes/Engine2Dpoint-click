using Engine.Math;
using Engine.Scene.Definitions;

namespace Engine.Scene.Builders;

public class SceneSpawnBuilder
{
    private readonly SceneData _scene;


    public SceneSpawnBuilder(
        SceneData scene)
    {
        _scene = scene;
    }


    public void Add(
        string id,
        Vector2 position)
    {
        _scene.SpawnPoints.Add(
            new SpawnPointData
            {
                Id = id,
                Position = position
            }
        );
    }

    public void AddDefault(Vector2 position)
    {
        Add(
            "Default",
            position
        );
    }
}