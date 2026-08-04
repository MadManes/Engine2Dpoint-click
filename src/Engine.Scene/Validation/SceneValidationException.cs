using System.Text;

namespace Engine.Scene.Validation;

public class SceneValidationException : Exception
{
    public ValidationResult Result { get; }

    public SceneValidationException(
        ValidationResult result)
        : base(CreateMessage(result))
    {
        Result = result;
    }

    private static string CreateMessage(
        ValidationResult result)
    {
        StringBuilder builder = new();

        builder.AppendLine(
            "Scene validation failed."
        );

        foreach (ValidationMessage message in result.Messages)
        {
            builder.AppendLine(
                $"[{message.Code}] {message.Message}"
            );
        }

        return builder.ToString();
    }
}