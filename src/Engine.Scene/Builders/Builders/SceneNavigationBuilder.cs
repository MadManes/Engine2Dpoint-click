using Engine.Scene.Definitions;

namespace Engine.Scene.Builders;

public class SceneNavigationBuilder
{
    private readonly SceneData _scene;


    public SceneNavigationBuilder(
        SceneData scene)
    {
        _scene = scene;
    }


    public void SetNavigation(
        NavigationData navigation)
    {
        _scene.Navigation = navigation;
    }
}