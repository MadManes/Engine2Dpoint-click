using Engine.Scene.Definitions;
using Engine.Entities.Components;

namespace Engine.Editor.Creation;

public class EditorEntityFactory
{
    public EntityData CreateGeneric()
    {
        return new EntityData
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Entity",
            Type = EntityType.Generic
        };
    }
}