using Engine.Entities;

namespace Engine.Runtime.Interaction;

public class InteractionSystem
{
    public void Interact(Entity entity)
    {
        Console.WriteLine(
            $"Interactuando con: {entity.Name}"
        );
    }
}