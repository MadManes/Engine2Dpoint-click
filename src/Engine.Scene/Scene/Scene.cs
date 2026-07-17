using Engine.Entities;

namespace Engine.Scene.World;

public class Scene : IScene
{
    private readonly List<Entity> _entities = new();
    public World World { get; }

    public string Name { get; }

    public IReadOnlyList<Entity> Entities => _entities;

    public Scene(string name, World world)
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