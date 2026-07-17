namespace Engine.Scene;

using Engine.Math;

public class WalkableArea : IWalkableArea
{
    public NavigationType NavigationType { get; }

    public WalkableArea(NavigationType navigationType)
    {
        NavigationType = navigationType;
    }

    public bool Contains(Vector2 position)
    {
        return true;
    }
}