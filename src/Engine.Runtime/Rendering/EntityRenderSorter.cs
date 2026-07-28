using Engine.Entities;

namespace Engine.Runtime.Rendering;

public class EntityRenderSorter
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