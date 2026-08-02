using Engine.Entities;

namespace Engine.Runtime.Interaction;

public class InteractionSystem
{
    private InteractionRequest? _pendingInteraction;

    private readonly InteractionExecutor _executor;

    public InteractionSystem(
        InteractionExecutor executor)
    {
        _executor = executor;
    }

    public void BeginInteraction(
        InteractionRequest request)
    {
        _pendingInteraction = request;
    }

    public void Update(Entity player)
    {
        if (_pendingInteraction == null)
        {
            return;
        }

        if (player.Movement.IsMoving())
        {
            return;
        }

        if (!player.Movement.HasFinishedPath)
        {
            return;
        }

        ExecuteInteraction(
            _pendingInteraction
        );

        _pendingInteraction = null;

        player.Movement.ResetFinishedPath();
    }

    private void ExecuteInteraction(
        InteractionRequest request)
    {
        _executor.Execute(request);
    }
}
        