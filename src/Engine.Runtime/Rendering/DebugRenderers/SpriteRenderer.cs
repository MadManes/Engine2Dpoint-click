using Engine.Math;
using Engine.Entities;
using Engine.Assets;
using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class SpriteRenderer
{
    private readonly AssetManager _assetManager;
    private readonly SpritePlacement _placement;

    public SpriteRenderer(
        AssetManager assetManager)
    {
        _assetManager = assetManager;
        _placement = new SpritePlacement();
    }


    public void Draw(Entity entity)
    {
        if (!entity.Visible)
        {
            return;
        }

        Texture2D texture =
            _assetManager.LoadSprite(
                entity.Sprite.Set.Idle + ".png"
            );

        Vector2 drawPosition = _placement.GetDrawPosition(entity);
        
        
        Raylib.DrawTexture(
            texture,
            (int)drawPosition.X,
            (int)drawPosition.Y,
            Color.White
        );
    }
}