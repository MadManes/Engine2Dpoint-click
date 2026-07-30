namespace Engine.Game.State;

public class EntityState
{
    private readonly Dictionary<string, bool> _flags = new();

    public string EntityId { get; }
    public bool Exists { get; set; } = true;

    public EntityState(string entityId)
    {
        EntityId = entityId;
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