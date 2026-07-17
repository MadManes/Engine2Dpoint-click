namespace Engine.Scene;

public class World
{
    public IWalkableArea WalkableArea { get; }

    public World(IWalkableArea walkableArea)
    {
        WalkableArea = walkableArea;
    }
}