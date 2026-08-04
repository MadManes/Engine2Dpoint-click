using Engine.Scene.Definitions;

namespace Engine.Scene.Validation;

public interface IProjectValidator
{
    ValidationResult Validate(
        SceneProject project
    );
}