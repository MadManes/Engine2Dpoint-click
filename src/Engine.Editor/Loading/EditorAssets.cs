using Engine.Assets;
using Raylib_cs;

namespace Engine.Editor.Loading;

public class EditorAssets
{
    public Texture2D Background { get; }

    
    private readonly AssetManager _assetManager;

    private readonly Dictionary<string, Texture2D> _sprites;
    private readonly IReadOnlyCollection<string> _spriteNames;


    public EditorAssets(
        Texture2D background,
        Dictionary<string, Texture2D> sprites,
        IReadOnlyCollection<string> spriteNames,
        AssetManager assetManager)
    {
        Background = background;

        _sprites = sprites;
        _spriteNames = spriteNames;

        _assetManager = assetManager;
    }


    public Texture2D GetSprite(
        string assetName)
    {
        if(!_sprites.TryGetValue(
            assetName,
            out Texture2D sprite))
        {
            sprite =
                _assetManager.LoadSprite(
                    assetName + ".png"
                );

            _sprites.Add(
                assetName,
                sprite
            );
        }

        return sprite;
    }


    public IEnumerable<string> SpriteNames =>
        _spriteNames;


}
