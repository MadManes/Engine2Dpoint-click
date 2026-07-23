using Engine.Math;
using Engine.Scene.World;
using Engine.Core;

namespace Engine.Navigation;

public class NavigationRequest
{
    public SceneWorld World { get; }

    public Vector2 Start { get; }

    public Vector2 Destination { get; }

    public NavigationRequest(
        SceneWorld world,
        Vector2 start,
        Vector2 destination)
    {
        World = world;
        Start = start;
        Destination = destination;
    }
}