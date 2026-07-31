using Engine.Entities;
using Engine.Game.State;

namespace Engine.Runtime.Systems;

public class EntityStateApplier
{
    public void Apply(
        Entity entity,
        EntityState state)
    {
        if (state.State != "Default")
        {
            Console.WriteLine(
                $"{entity.Name} tiene estado {state.State}"
            );
        }
    }
}