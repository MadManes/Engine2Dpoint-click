using Raylib_cs;

namespace Engine.Assets;

public class LoadedSceneAssets
{
    public Texture2D Background { get; }

    public LoadedSceneAssets(
        Texture2D background)
    {
        Background = background;
    }
}