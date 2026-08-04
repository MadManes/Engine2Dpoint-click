using Engine.Scene.Definitions;

namespace Engine.Scene.Builders;

public class SceneBackgroundBuilder
{
    private readonly SceneData _scene;

    public SceneBackgroundBuilder(
        SceneData scene)
    {
        _scene = scene;
    }

    public void SetAsset(string assetName)
    {
        _scene.Background.AssetName = assetName;
    }
}