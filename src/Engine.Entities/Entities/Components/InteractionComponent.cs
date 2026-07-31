namespace Engine.Entities.Components;

public class InteractionComponent : Component
{
    public bool Enabled { get; }

    public InteractionComponent(
        bool enabled = true)
    {
        Enabled = enabled;
    }
}