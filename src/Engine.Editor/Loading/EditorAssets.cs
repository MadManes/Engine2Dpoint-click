using Raylib_cs;

namespace Engine.Editor.Loading;

public class EditorAssets
{
    public Texture2D Background { get; }    

    private readonly Dictionary<string, Texture2D> _sprites;


    public EditorAssets(
        Texture2D background,
        Dictionary<string, Texture2D> sprites)
    {
        Background = background;
        _sprites = sprites;
    }


    public Texture2D GetSprite(
        string assetName)
    {
        return _sprites[assetName];
    }

    public IEnumerable<string> SpriteNames =>
        _sprites.Keys;
}