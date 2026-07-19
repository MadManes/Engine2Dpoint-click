using Engine.Math;

namespace Engine.Entities.Components;

public class Movement : Component
{
    public Vector2? Destination { get; private set; }

    public float Speed { get; set; }

    public Movement()
    {
        Destination = null;
        Speed = 120f;
    }

    public void SetDestination(Vector2 destination)
    {
        Destination = destination;
    }

    public void ClearDestination()
    {
        Destination = null;
    }
}