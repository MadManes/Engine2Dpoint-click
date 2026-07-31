namespace Engine.Game.State;

public class EntityState
{
    private readonly Dictionary<string, bool> _flags = new();

    public string EntityId { get; }

    public string State { get; private set; } = "Default";
    public bool Exists { get; private set; } = true;

    public EntityState(string entityId)
    {
        EntityId = entityId;
    }

    public void SetState(string state)
    {
        State = state;
    }

    public bool IsState(string state)
    {
        return State == state;
    }

    public void Remove()
    {
        Exists = false;
    }

    public void Restore()
    {
        Exists = true;
    }

    public void SetFlag(
        string flag,
        bool value)
    {
        _flags[flag] = value;
    }

    public bool GetFlag(
        string flag)
    {
        return _flags.TryGetValue(
            flag,
            out bool value
        )
        && value;
    }    
}