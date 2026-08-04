using Engine.Scene.Definitions;

namespace Engine.Scene.Validation;

public interface ISceneValidator
{
    ValidationResult Validate(SceneData scene);
}