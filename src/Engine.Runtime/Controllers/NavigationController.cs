using Engine.Math;
using Engine.Scene.World;

namespace Engine.Runtime.Controllers;

public class NavigationController
{
    public Vector2 GetDestination(
        World world,
        Vector2 clickPosition)
    {
        if (world.WalkableArea.Contains(clickPosition))
        {
            return clickPosition;
        }

        return world.WalkableArea.GetClosestPoint(clickPosition);
    }    
}