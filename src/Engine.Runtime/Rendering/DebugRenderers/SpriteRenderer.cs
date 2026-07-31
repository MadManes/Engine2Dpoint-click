using Engine.Math;
using Engine.Entities;
using Engine.Runtime.Assets;
using Raylib_cs;

namespace Engine.Runtime.Rendering;

public class SpriteRenderer
{
    private readonly AssetManager _assetManager;

    public SpriteRenderer(
        AssetManager assetManager)
    {
        _assetManager = assetManager;
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

        float drawX =
            entity.Transform.Position.X
            - entity.Sprite.Pivot.X;
        
        float drawY =
            entity.Transform.Position.Y
            - entity.Sprite.Pivot.Y;
        
        
        Raylib.DrawTexture(
            texture,
            (int)drawX,
            (int)drawY,
            Color.White
        );
    }
}