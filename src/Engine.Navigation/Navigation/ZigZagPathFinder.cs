using Engine.Core;
using Engine.Math;

namespace Engine.Navigation;

public class ZigZagPathFinder : IPathFinder
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


        Vector2 direction =
            destination - start;


        float offset = 100f;


        Vector2 perpendicular =
            new Vector2(
                -direction.Y,
                direction.X
            )
            .Normalize();


        Vector2 point1 =
            start +
            direction * 0.2f +
            perpendicular * offset;


        Vector2 point2 =
            start +
            direction * 0.3f -
            perpendicular * offset;


        Vector2 point3 =
            start +
            direction * 0.85f +
            perpendicular * offset;


        return new PathBuilder()
            .AddPoint(start)
            .AddPoint(point1)
            .AddPoint(point2)
            .AddPoint(point3)
            .AddPoint(destination)
            .Build();
    }
}