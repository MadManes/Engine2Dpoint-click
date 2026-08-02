using Engine.Entities;

namespace Engine.Runtime.Interaction;

public interface IInteractionHandler
{
    void Execute(Entity entity);
}