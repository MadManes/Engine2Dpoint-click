using Engine.Scene.Definitions;

namespace Engine.Scene.Builders;

public class SceneIdentityBuilder
{
    private readonly SceneData _scene;

    public SceneIdentityBuilder(
        SceneData scene)
    {
        _scene = scene;
    }

    public void SetId(string id)
    {
        _scene.Id = id;
    }

    public void SetName(string name)
    {
        _scene.Name = name;
    }
}