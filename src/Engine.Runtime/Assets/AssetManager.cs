using Raylib_cs;
using Engine.Scene.Definitions;

namespace Engine.Runtime.Assets;

public class AssetManager
{
    private readonly string _assetPath;
    private readonly Dictionary<string, Texture2D> _textures = new();

    public AssetManager(string assetPath)
    {
        _assetPath = assetPath;
    }


    public string GetBackgroundPath(string assetName)
    {
        return Path.Combine(
            _assetPath,
            "backgrounds",
            assetName
        );
    }

    public Texture2D LoadTexture(string assetName)
    {
        if (_textures.TryGetValue(
            assetName,
            out Texture2D texture))
        {
            return texture;
        }
    
    
        string path = GetBackgroundPath(assetName);
    
        texture = Raylib.LoadTexture(path);
    
        _textures.Add(
            assetName,
            texture
        );
    
        return texture;
    }

    public Texture2D LoadSprite(string assetName)
    {
        if (_textures.TryGetValue(
            assetName,
            out Texture2D texture))
        {
            return texture;
        }
    
    
        string path = Path.Combine(
            _assetPath,
            "sprites",
            assetName
        );
    
    
        texture = Raylib.LoadTexture(path);
    
    
        _textures.Add(
            assetName,
            texture
        );
    
    
        return texture;
    }

    public LoadedSceneAssets LoadScene(SceneData scene)
    {
        Texture2D background = LoadTexture(
            scene.Background.AssetName + ".png"
        );

        return new LoadedSceneAssets(
            background
        );
    }

    public void Dispose()
    {
        foreach (Texture2D texture in _textures.Values)
        {
            Raylib.UnloadTexture(texture);
        }

        _textures.Clear();
    }

}