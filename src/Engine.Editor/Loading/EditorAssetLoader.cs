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


        Dictionary<string, Texture2D> sprites =
            new();


        foreach(var entity in document.Scene.EntityCollection.Entities)
        {
            string assetName =
                entity.Sprite.AssetName;


            if(string.IsNullOrWhiteSpace(assetName))
                continue;


            if(sprites.ContainsKey(assetName))
                continue;


            Texture2D sprite =
                _assetManager.LoadSprite(
                    assetName + ".png"
                );


            sprites.Add(
                assetName,
                sprite
            );
        }


        Console.WriteLine(
            $"Background: {background.Width}x{background.Height}"
        );


        Console.WriteLine(
            $"Sprites loaded: {sprites.Count}"
        );


        return new EditorAssets(
            background,
            sprites
        );
    }
}