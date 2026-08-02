using Engine.Entities;
using Engine.Scene.Definitions;
using Engine.Scene.World;

namespace Engine.Scene;

public class GameScene : IScene
{
    public SceneData Data { get; }

    public SceneWorld World { get; }

    public string Id { get; }

    public string Name { get; }


    public IReadOnlyList<Entity> Entities =>
        World.Entities;


    public GameScene(
        SceneData data,
        SceneWorld world)
    {
        Data = data;

        Id = data.Id;

        Name = data.Name;

        World = world;
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
    }
}