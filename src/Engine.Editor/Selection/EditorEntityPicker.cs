using Engine.Entities.Rendering;
using Engine.Math;
using Engine.Scene.Definitions;
using Engine.Assets;
using Raylib_cs;

namespace Engine.Editor.Selection;

public class EditorEntityPicker
{
    private readonly AssetManager _assetManager;
    private readonly SpritePlacement _spritePlacement;


    public EditorEntityPicker(
        AssetManager assetManager)
    {
        _assetManager = assetManager;
        _spritePlacement = new SpritePlacement();
    }


    public EntityData? Pick(
        IEnumerable<EntityData> entities,
        Vector2 position)
    {
        foreach(EntityData entity in entities.Reverse())
        {
            Texture2D texture =
                _assetManager.LoadSprite(
                    entity.Sprite.AssetName + ".png"
                );


            Vector2 drawPosition =
                new Vector2(
                    entity.Position.X - 16,
                    entity.Position.Y - 32
                );


            Engine.Math.Rectangle bounds =
                new Engine.Math.Rectangle(
                    drawPosition,
                    texture.Width,
                    texture.Height
                );


            if(bounds.Contains(position))
            {
                return entity;
            }
        }


        return null;
    }
}