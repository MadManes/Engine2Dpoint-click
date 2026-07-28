namespace Engine.Scene.Definitions;

public class EntityCollectionData
{
    public IReadOnlyList<EntityData> Entities { get; }

    public EntityCollectionData(
        IReadOnlyList<EntityData> entities)
    {
        Entities = entities;
    }
}
