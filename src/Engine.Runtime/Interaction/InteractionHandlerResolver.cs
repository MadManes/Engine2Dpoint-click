using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Runtime.Interaction;

public class InteractionHandlerResolver
{
    private readonly IInteractionHandler _defaultHandler;
    private readonly IInteractionHandler _doorHandler;

    public InteractionHandlerResolver(
        IInteractionHandler defaultHandler,
        IInteractionHandler doorHandler)
    {
        _defaultHandler = defaultHandler;
        _doorHandler = doorHandler;
    }

    public IInteractionHandler Resolve(Entity entity)
    {
        if (entity.Components.Has<DoorComponent>())
        {
            return _doorHandler;
        }

        return _defaultHandler;
    }
}