using Engine.Entities;
using Engine.Scene.World;

namespace Engine.Scene;

public class GameScene : IScene
{
    private readonly List<Entity> _entities = new();
    public SceneWorld World { get; }

    public string Name { get; }

    public IReadOnlyList<Entity> Entities => _entities;

    public GameScene(string name, SceneWorld world)
    {
        Name = name;
        World = world;
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