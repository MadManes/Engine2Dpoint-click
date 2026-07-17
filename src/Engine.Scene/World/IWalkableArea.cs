using Engine.Math;

namespace Engine.Scene.World;

public interface IWalkableArea
{
    NavigationType NavigationType { get; }

    bool Contains(Vector2 position);

    Vector2 GetClosestPoint(Vector2 position);
    
}