using Engine.Entities;
using Engine.Entities.Components;
using Engine.Scene.Definitions;
using Engine.Scene.Factories;
using Engine.Scene.World;

namespace Engine.Scene;

public class GameScene : IScene
{
    private readonly List<Entity> _entities = new();

    public SceneData Data { get; }

    public SceneWorld World { get; }    

    public string Name { get; }

    public IReadOnlyList<Entity> Entities => _entities;

    public Entity Player
    {
        get
        {
            return World.Entities.First(
                entity => entity.Type == EntityType.Player
            );
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
    
            World.AddEntity(entity);
            AddEntity(entity);
        }
    }  

    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }


    public void RemoveEntity(Entity entity)
    {
        _entities.Remove(entity);
    }

    public void AddPersistentEntity(Entity entity)
    {
        if (!_entities.Contains(entity))
        {
            _entities.Add(entity);
        }
    
        if (!World.Entities.Contains(entity))
        {
            World.AddEntity(entity);
        }
    }
}