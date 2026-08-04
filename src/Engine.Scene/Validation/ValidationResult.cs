namespace Engine.Scene.Validation;

public class ValidationResult
{
    private readonly List<ValidationMessage> _messages = new();

    public IReadOnlyList<ValidationMessage> Messages => _messages;


    public bool IsValid =>
        !_messages.Any(
            x => x.Severity == 
                ValidationSeverity.Error
        );

    public void AddError(
        string code,
        string message)
    {
        _messages.Add(
            new ValidationMessage(
                ValidationSeverity.Error,
                code,
                message));
    }

    public void AddWarning(
        string code,
        string message)
    {
        _messages.Add(
            new ValidationMessage(
                ValidationSeverity.Warning,
                code,
                message));
    }

    public void AddInfo(
        string code,
        string message)
    {
        _messages.Add(
            new ValidationMessage(
                ValidationSeverity.Info,
                code,
                message));
    }
}
    
