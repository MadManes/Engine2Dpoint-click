using Engine.Entities;

namespace Engine.Scene;

public class Scene : IScene
{
    private readonly List<Entity> _entities = new();

    public string Name { get; }

    public IReadOnlyList<Entity> Entities => _entities;

    public Scene(string name)
    {
        Name = name;
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