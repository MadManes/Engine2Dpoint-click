using Engine.Entities.Sprites;

namespace Engine.Entities.Components;

public class SpriteComponent : Component
{
    public SpriteSet Set { get; }
    public SpritePivot Pivot { get; }

    public SpriteComponent(
        SpriteSet set,
        SpritePivot pivot
        )
    {
        Set = set;
        Pivot = pivot;
    }
}