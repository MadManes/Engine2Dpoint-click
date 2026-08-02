using Engine.Scene.Definitions;
using Engine.Scene.Factories;
using Engine.Scene.Loading;

namespace Engine.Scene.Scenes;

public class SceneRegistry
{
    private readonly SceneRepository _repository;
    private readonly SceneFactory _factory;


    public SceneRegistry(
        SceneRepository repository,
        SceneFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }


    public GameScene Create(
        string name)
    {
        SceneData data =
            _repository.Get(name);


        return _factory.Create(data);
    }
}