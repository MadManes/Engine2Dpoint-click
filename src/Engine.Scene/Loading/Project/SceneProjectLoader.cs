using Engine.Scene.Definitions;
using Engine.Scene.Validation;

namespace Engine.Scene.Loading.Project;

public class SceneProjectLoader
{
    private readonly ISceneSource _source;
    private readonly IProjectValidator _validator;


    public SceneProjectLoader(
        ISceneSource source,
        IProjectValidator validator)
    {
        _source = source;
        _validator = validator;
    }


    public SceneProject Load()
    {
        List<SceneData> scenes = new();


        foreach(string id in _source.Names)
        {
            scenes.Add(
                _source.Load(id)
            );
        }


        SceneProject project =
            new(scenes);


        ValidationResult result =
            _validator.Validate(project);


        if(!result.IsValid)
        {
            throw new SceneValidationException(
                result
            );
        }


        return project;
    }
}