using Engine.Assets;
using Engine.Editor.Documents;
using Raylib_cs;

namespace Engine.Editor.Loading;

public class EditorAssetLoader
{
    private readonly AssetManager _assetManager;
    private readonly string _spriteFolder;

    
    public EditorAssetLoader(
        AssetManager assetManager,
        string spriteFolder)
    {
        _assetManager = assetManager;
        _spriteFolder = spriteFolder;
    }


    public EditorAssets Load(
        SceneDocument document)
    {
        Texture2D background = default;

        string backgroundAssetName =
            document.Scene.Background.AssetName;
        
        if(!string.IsNullOrWhiteSpace(
            backgroundAssetName))
        {
            background =
                _assetManager.LoadBackground(
                    backgroundAssetName + ".png"
                );
        }


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


        List<string> spriteNames =
            new();


        if(Directory.Exists(_spriteFolder))
        {
            foreach(string file in
                Directory.GetFiles(
                    _spriteFolder,
                    "*.png"))
            {
                string assetName =
                    Path.GetFileNameWithoutExtension(
                        file
                    );

                spriteNames.Add(
                    assetName
                );
            }
        }


        Console.WriteLine(
            $"Background: {background.Width}x{background.Height}"
        );


        Console.WriteLine(
            $"Sprites loaded: {sprites.Count}"
        );


        Console.WriteLine(
            $"Sprites available: {spriteNames.Count}"
        );


        return new EditorAssets(
            background,
            sprites,
            spriteNames,
            _assetManager
        );
    }


}
