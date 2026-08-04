namespace Engine.Scene.Validation;

public class ValidationMessage
{
    public ValidationSeverity Severity { get; }

    public string Code { get; }

    public string Message { get; }


    public ValidationMessage(
        ValidationSeverity severity,
        string code,
        string message)
    {
        Severity = severity;
        Code = code;
        Message = message;
    }
}