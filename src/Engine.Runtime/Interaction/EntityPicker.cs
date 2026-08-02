using Engine.Math;
using Engine.Entities;
using Engine.Runtime.Assets;
using Engine.Runtime.Rendering;
using Raylib_cs;

namespace Engine.Runtime.Interaction;

public class EntityPicker
{
    private readonly EntityVisualSorter _visualSorter;
    private readonly SpritePlacement _spritePlacement;
    private readonly AssetManager _assetManager;
    
    public EntityPicker(
        AssetManager assetManager
    )
    {
        _visualSorter = new EntityVisualSorter();
        _spritePlacement = new SpritePlacement();
        _assetManager = assetManager;
    }

    public Entity? Pick(
        IEnumerable<Entity> entities,
        Vector2 worldPosition)
    {
        Console.WriteLine(
            $"Buscando entidad en {worldPosition.X}, {worldPosition.Y}"
        );

        IEnumerable<Entity> orderedEntities =
            _visualSorter
                .Sort(entities)
                .Reverse();

        foreach (Entity entity in orderedEntities)
        {
            if (entity.Interaction == null)
            {
                continue;
            }
            
            Console.WriteLine(
                $"Revisando: {entity.Name}"
            );

            Texture2D texture =
                _assetManager.LoadSprite(
                    entity.Sprite.Set.Idle + ".png"
                );


            Vector2 drawPosition =
                _spritePlacement.GetDrawPosition(entity);


            Engine.Math.Rectangle bounds = new Engine.Math.Rectangle(
                drawPosition,
                texture.Width,
                texture.Height
            );

            Console.WriteLine(
                $"{entity.Name} Rect: " +
                $"{bounds.Position.X},{bounds.Position.Y} " +
                $"{bounds.Width}x{bounds.Height}"
            );


            if (bounds.Contains(worldPosition))
            {
                Console.WriteLine(
                    $"Entidad seleccionada: {entity.Name}"
                );

                return entity;
            }
        }


        return null;
    }
}
