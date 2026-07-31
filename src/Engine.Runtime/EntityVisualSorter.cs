using Engine.Entities;

namespace Engine.Runtime;

public class EntityVisualSorter
{
    public IEnumerable<Entity> Sort(
        IEnumerable<Entity> entities)
    {
        return entities
            .OrderBy(entity =>
                entity.Transform.Position.Y
            );
    }
}