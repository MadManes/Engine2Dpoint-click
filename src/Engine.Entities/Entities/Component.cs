namespace Engine.Entities;

public abstract class Component
{
    public Entity Entity { get; internal set; } = null!;
}