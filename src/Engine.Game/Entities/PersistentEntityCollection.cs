using Engine.Entities;

namespace Engine.Game.Entities;

public class PersistentEntityCollection
{
    private readonly List<Entity> _entities = new();

    public IReadOnlyList<Entity> Entities => _entities;


    public void Add(Entity entity)
    {
        if (!_entities.Contains(entity))
        {
            _entities.Add(entity);
        }
    }


    public void Remove(Entity entity)
    {
        _entities.Remove(entity);
    }


    public bool Contains(Entity entity)
    {
        return _entities.Contains(entity);
    }
}