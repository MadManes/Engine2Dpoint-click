using Engine.Assets;
using Engine.Editor.Documents;
using Raylib_cs;

namespace Engine.Editor.Loading;

public class EditorAssetLoader
{
    private readonly AssetManager _assetManager;

    public EditorAssetLoader(
        AssetManager assetManager)
    {
        _assetManager = assetManager;
    }

    public EditorAssets Load(
    SceneDocument document)
    {
        Texture2D background =
            _assetManager.LoadBackground(
                document.Scene.Background.AssetName + ".png"
            );

        Console.WriteLine(
            $"Background: {background.Width}x{background.Height}"
        );

        return new EditorAssets(background);
    }

    //public EditorAssets Load(
    //    SceneDocument document)
    //{
    //   return new EditorAssets(
    //        _assetManager.LoadBackground(
    //            document.Scene.Background.AssetName + ".png"
    //        )
    //    );
    //}
}