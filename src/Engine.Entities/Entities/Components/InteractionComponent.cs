namespace Engine.Entities.Components;

public class InteractionComponent : Component
{
    public bool Enabled { get; }
    public InteractionLocation Location { get; }
    
    public InteractionComponent(
        bool enabled = true)
    {
        Enabled = enabled;

        Location = new InteractionLocation();
    }
}