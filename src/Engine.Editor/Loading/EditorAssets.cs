using Raylib_cs;

namespace Engine.Editor.Loading;

public class EditorAssets
{
    public Texture2D Background { get; }

    public EditorAssets(
        Texture2D background)
    {
        Background = background;
    }
}