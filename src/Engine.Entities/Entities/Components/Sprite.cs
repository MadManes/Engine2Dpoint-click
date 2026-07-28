namespace Engine.Entities.Components;

public class Sprite
{
    public string AssetName { get; }

    public Sprite(string assetName)
    {
        AssetName = assetName;
    }
}