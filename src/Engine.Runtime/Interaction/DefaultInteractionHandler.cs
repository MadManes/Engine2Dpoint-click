using Engine.Entities;

namespace Engine.Runtime.Interaction;

public class DefaultInteractionHandler
    : IInteractionHandler
{
    public void Execute(Entity entity)
    {
        Console.WriteLine(
            $"Interactuando con: {entity.Name}"
        );
    }
}