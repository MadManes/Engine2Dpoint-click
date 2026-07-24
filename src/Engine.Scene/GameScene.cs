using Engine.Entities;
using Engine.Scene.Definitions;
using Engine.Scene.World;

namespace Engine.Scene;

public class GameScene : IScene
{
    private readonly List<Entity> _entities = new();

    public SceneData Data { get; }

    public SceneWorld World { get; }

    public string Name { get; }

    public IReadOnlyList<Entity> Entities => _entities;


    public GameScene(SceneData data)
    {
        Data = data;

        Name = data.Name;

        World = WorldFactory.Create(data);
    }


    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }


    public void RemoveEntity(Entity entity)
    {
        _entities.Remove(entity);
    }
}