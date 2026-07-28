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
        Texture2D texture =
            _assetManager.LoadSprite(
                entity.Sprite.AssetName + ".png"
            );

        Vector2 position = entity.Transform.Position;
        int x = (int)(
            position.X - texture.Width / 2
        );
        int y = (int)(
            position.Y - texture.Height
        );

        Raylib.DrawTexture(
            texture,
            x,
            y,
            Color.White
        );
    }
}