using Engine.Math;
using Engine.Core;
using Engine.Scene.World;
using Engine.Navigation;

namespace Engine.Runtime.Controllers;

public class NavigationController
{
    private readonly IPathFinder _pathFinder;

    public NavigationController(
        IPathFinder pathFinder)
    {
        _pathFinder = pathFinder;
    }


    public NavigationPath GetPath(
        SceneWorld world,
        Vector2 start,
        Vector2 destination)
    {
        NavigationRequest request = new(
            world,
            start,
            destination
        );

        return _pathFinder.FindPath(request);
    }
}