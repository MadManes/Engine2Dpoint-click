using Engine.Scene.Definitions;
using Engine.Scene.Validation;

namespace Engine.Scene.Builders;

public class SceneBuilder
{
    private readonly SceneValidator _validator = new();
    private readonly SceneData _scene;


    public SceneIdentityBuilder Identity { get; }
    public SceneBackgroundBuilder Background { get; }
    public SceneNavigationBuilder Navigation { get; }
    public SceneSpawnBuilder SpawnPoints { get; }
    public SceneEntityBuilder Entities { get; }

    public SceneBuilder()
    {
        _scene = new SceneData();

        Identity = new SceneIdentityBuilder(_scene);
        Background = new SceneBackgroundBuilder(_scene);
        Navigation = new SceneNavigationBuilder(_scene);
        SpawnPoints = new SceneSpawnBuilder(_scene);
        Entities = new SceneEntityBuilder(_scene);
    }

    public SceneBuilder(SceneData scene)
    {
        _scene = scene;

        Identity = new SceneIdentityBuilder(_scene);
        Background = new SceneBackgroundBuilder(_scene);
        Navigation = new SceneNavigationBuilder(_scene);
        SpawnPoints = new SceneSpawnBuilder(_scene);
        Entities = new SceneEntityBuilder(_scene);
    }

    public SceneData Build()
    {
        ValidationResult result =
            _validator.Validate(_scene);

        if (!result.IsValid)
        {
            throw new SceneValidationException(result);
        }

        return _scene;
    }

}