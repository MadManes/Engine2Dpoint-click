using Engine.Math;
using Engine.Entities;

namespace Engine.Runtime.Rendering;

public class SpritePlacement
{
    public Vector2 GetDrawPosition(Entity entity)
    {
        return new Vector2(
            entity.Transform.Position.X - entity.Sprite.Pivot.X,
            entity.Transform.Position.Y - entity.Sprite.Pivot.Y
        );
    }
}