using Engine.Entities;
using Engine.Scene.Definitions;
using Engine.Scene.Factories;
using Engine.Scene.World;

namespace Engine.Scene;

public class GameScene : IScene
{
    public SceneData Data { get; }

    public SceneWorld World { get; }    

    public string Name { get; }

    public IReadOnlyList<Entity> Entities
    {
        get
        {
            return World.Entities;
        }
    }

    
    public GameScene(SceneData data)
    {
        Data = data;
        Name = data.Name;
        World = WorldFactory.Create(data);

        CreateEntities(data);
    }

    private void CreateEntities(SceneData data)
    {
        foreach (EntityData entityData in data.EntityCollection.Entities)
        {
            Entity entity =
                EntityFactory.Create(entityData);    
            
            AddEntity(entity);
        }
    }  

    public void AddEntity(Entity entity)
    {
        World.AddEntity(entity);
    }


    public void RemoveEntity(Entity entity)
    {
        World.RemoveEntity(entity);
    }

    public void AddPersistentEntity(Entity entity)
    {
        if (!World.Contains(entity))
        {
            World.AddEntity(entity);
        }

        if (!World.Entities.Contains(entity))
        {
            World.AddEntity(entity);
        }
    }
}