using Engine.Entities;

namespace Engine.Scene.World;

public interface IScene
{
    string Name { get; }

    IReadOnlyList<Entity> Entities { get; }

    void AddEntity(Entity entity);

    void RemoveEntity(Entity entity);
}