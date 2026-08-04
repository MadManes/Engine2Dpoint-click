using Engine.Entities.Components;
using Engine.Math;
using Engine.Scene.Definitions;

namespace Engine.Scene.Builders;

public class SceneEntityBuilder
{
    private readonly SceneData _scene;


    public SceneEntityBuilder(
        SceneData scene)
    {
        _scene = scene;
    }


    public void Add(
        string id,
        string name,
        Vector2 position,
        EntityType type)
    {
        _scene.EntityCollection.Entities.Add(
            new EntityData
            {
                Id = id,
                Name = name,
                Position = position,
                Type = type
            }
        );
    }
}