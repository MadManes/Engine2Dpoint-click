using Engine.Entities;
using Engine.Entities.Components;
using Engine.Scene.Definitions;

namespace Engine.Scene.Factories;

public static class EntityFactory
{
    public static Entity Create(EntityData data)
    {
        Sprite sprite = new(
            data.Sprite.AssetName
        );
        
        Entity entity = new(
            data.Name,
            data.Type,
            sprite
        );

        entity.Transform.SetPosition(
            data.Position
        );

        return entity;
    }
}