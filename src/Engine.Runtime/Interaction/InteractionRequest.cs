using Engine.Entities;

namespace Engine.Runtime.Interaction;

public class InteractionRequest
{
    public Entity Entity { get; }

    public InteractionAction? Action { get; }


    public InteractionRequest(
        Entity entity,
        InteractionAction? action = null)
    {
        Entity = entity;
        Action = action;
    }
}