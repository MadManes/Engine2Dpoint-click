using Engine.Entities;

namespace Engine.Scene.World;

public class SceneWorld
{
    public IWalkableArea WalkableArea { get; }

    private readonly List<Entity> _entities = new();
    public IReadOnlyList<Entity> Entities => _entities;

    public SceneWorld(IWalkableArea walkableArea)
    {
        WalkableArea = walkableArea;
    }

    public void AddEntity(Entity entity)
    {
        if (_entities.Contains(entity))
        {
            return;
        }

        _entities.Add(entity);
    }

    public void RemoveEntity(Entity entity)
    {
        _entities.Remove(entity);
    }

    public bool Contains(Entity entity)
    {
        return _entities.Contains(entity);
    }
    
}