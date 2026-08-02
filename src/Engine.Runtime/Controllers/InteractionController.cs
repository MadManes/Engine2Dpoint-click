using Engine.Core;
using Engine.Math;
using Engine.Entities;
using Engine.Runtime.Interaction;
using Engine.Scene.World;

namespace Engine.Runtime.Controllers;

public class InteractionController
{
    private readonly NavigationController _navigationController;    
    private readonly InteractionSystem _interactionSystem;
    private readonly PlayerController _playerController;

    public InteractionController(
        NavigationController navigationController,
        InteractionSystem interactionSystem,
        PlayerController playerController)
    {
        _navigationController = navigationController;
        _interactionSystem = interactionSystem;
        _playerController = playerController;
    }

    public void HandleEntityClick(
        Entity entity,
        Entity player,
        SceneWorld world)
    {
        Vector2 target =
            entity.Interaction?.Location.Point
            ?? entity.Transform.Position;

        NavigationPath path =
            _navigationController.GetPath(
                world,
                player.Transform.Position,
                target
            );

        if (!path.IsEmpty)
        {
            _playerController.MoveTo(path);
        }

        InteractionRequest request = new(
            entity
        );

        _interactionSystem.BeginInteraction(
            request
        );
    }
}