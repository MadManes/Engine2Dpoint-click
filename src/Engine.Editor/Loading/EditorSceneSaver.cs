using Engine.Editor.Documents;
using Engine.Scene.Serialization;
using Engine.Scene.Validation;

namespace Engine.Editor.Loading;

public class EditorSceneSaver
{
    private readonly string _sceneFolder;
    private readonly SceneSerializer _serializer;
    private readonly SceneValidator _validator;

    public EditorSceneSaver(
        string sceneFolder)
    {
        _sceneFolder = sceneFolder;
        _serializer = new SceneSerializer();
        _validator = new SceneValidator();
    }

    public ValidationResult Validate(
        SceneDocument document)
    {
        return _validator.Validate(
            document.Scene
        );
    }


    public void Save(
        SceneDocument document)
    {
        ValidationResult result =
            _validator.Validate(
                document.Scene
            );
        
        if(!result.IsValid)
        {
            throw new SceneValidationException(
                result
            );
        }

        string path =
            Path.Combine(
                _sceneFolder,
                document.FileName
            );

        string json =
            _serializer.Serialize(
                document.Scene
            );

        File.WriteAllText(
            path,
            json
        );
    }
}