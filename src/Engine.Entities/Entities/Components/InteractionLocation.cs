using Engine.Math;

namespace Engine.Entities.Components;

public class InteractionLocation
{
    public Vector2? Point { get; }

    public InteractionLocation(Vector2? point = null)
    {
        Point = point;
    }
}