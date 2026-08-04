using Engine.Scene.Definitions;
using Engine.Scene.Validation;
using Engine.Scene.Loading.Writers;

namespace Engine.Scene.Exporting;

public class SceneExporter
{
    private readonly SceneValidator _validator;
    private readonly JsonSceneWriter _writer;


    public SceneExporter(
        SceneValidator validator,
        JsonSceneWriter writer)
    {
        _validator = validator;
        _writer = writer;
    }


    public void Export(SceneData scene)
    {
        ValidationResult result =
            _validator.Validate(scene);

        if (!result.IsValid)
        {
            throw new SceneValidationException(result);
        }


        _writer.Save(scene);
    }
}