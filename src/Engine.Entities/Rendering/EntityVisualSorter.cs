namespace Engine.Entities.Rendering;

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