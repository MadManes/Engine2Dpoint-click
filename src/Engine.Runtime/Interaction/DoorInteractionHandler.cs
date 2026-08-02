using Engine.Entities;
using Engine.Entities.Components;
using Engine.Runtime.SceneTransition;
using Engine.Scene.Scenes;

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
            SceneCatalog.Create(
                door.TargetScene
            ),
            door.TargetSpawn
        );
    }
}