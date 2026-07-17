namespace Engine.Scene;

using Engine.Math;

public interface IWalkableArea
{
    NavigationType NavigationType { get; }

    bool Contains(Vector2 position);
}