namespace Engine.Runtime.Interaction;

public class InteractionExecutor
{
    private readonly InteractionHandlerResolver _resolver;

    public InteractionExecutor(
        InteractionHandlerResolver resolver
    )
    {
        _resolver = resolver;
    }

    public void Execute(
        InteractionRequest request)
    {
        IInteractionHandler handler =
            _resolver.Resolve(request.Entity);

        handler.Execute(request.Entity);
    }
}
