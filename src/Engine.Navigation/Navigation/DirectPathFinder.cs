using Engine.Math;
using Engine.Core;

namespace Engine.Navigation;

public class DirectPathFinder : IPathFinder
{
    public NavigationPath FindPath(
        NavigationRequest request)
    {
        Vector2 destination = request.Destination;

        if (!request.World.WalkableArea.Contains(destination))
        {
            destination =
                request.World.WalkableArea.GetClosestPoint(destination);
        }
        
        Vector2 start = request.Start;
        Vector2 middle = (start + destination) / 2f;

        return new PathBuilder()
            .AddPoint(start)
            .AddPoint(middle)
            .AddPoint(destination)
            .Build();
    }
}