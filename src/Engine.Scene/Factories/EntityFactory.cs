using Engine.Entities;
using Engine.Entities.Components;
using Engine.Entities.Sprites;
using Engine.Scene.Definitions;

namespace Engine.Scene.Factories;

public static class EntityFactory
{
    public static Entity Create(EntityData data)
    {
        SpriteSet spriteSet = new(
            data.Sprite.AssetName
        );
        
        SpritePivot spritePivot = new(
            16,
            32
        );

        SpriteComponent sprite = new(
            spriteSet,
            spritePivot
        );
        
        Entity entity = new(
            data.Id,
            data.Name,
            data.Type,
            sprite
        );

        if (data.IsInteractable)
        {
            entity.EnableInteraction();
        }

        entity.Transform.SetPosition(
            data.Position
        );

        return entity;
    }
}