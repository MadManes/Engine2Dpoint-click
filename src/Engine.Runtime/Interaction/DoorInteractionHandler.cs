using Engine.Entities;
using Engine.Entities.Components;
using Engine.Runtime.SceneTransition;

namespace Engine.Runtime.Interaction;

public class DoorInteractionHandler
    : IInteractionHandler
{
    private readonly SceneTransitionService
        _transitionService;

    public DoorInteractionHandler(
        SceneTransitionService transitionService)
    {
        _transitionService = transitionService;
    }

    public void Execute(Entity entity)
    {
        DoorComponent door =
            entity.Components.Get<DoorComponent>()!;

        _transitionService.ChangeScene(
            door.TargetScene,
            door.TargetSpawn
        );
    }
}