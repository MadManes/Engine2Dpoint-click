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
        _entities.Add(entity);
    }
}