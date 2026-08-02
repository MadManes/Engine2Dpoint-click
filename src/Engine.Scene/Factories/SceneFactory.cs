using Engine.Entities;
using Engine.Scene.Definitions;
using Engine.Scene.World;

namespace Engine.Scene.Factories;

public class SceneFactory
{
    public GameScene Create(
        SceneData data)
    {
        SceneWorld world =
            WorldFactory.Create(data);


        GameScene scene =
            new GameScene(
                data,
                world
            );


        CreateEntities(
            scene,
            data
        );


        return scene;
    }


    private void CreateEntities(
        GameScene scene,
        SceneData data)
    {
        foreach(EntityData entityData in data.EntityCollection.Entities)
        {
            Entity entity =
                EntityFactory.Create(entityData);


            scene.AddEntity(entity);
        }
    }
}