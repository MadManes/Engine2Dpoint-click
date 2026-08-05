using Engine.Math;
using Engine.Scene.Definitions;

namespace Engine.Editor.Rendering;

public class EditorSpritePlacement
{
    public Vector2 GetDrawPosition(
        EntityData entity)
    {
        return new Vector2(
            entity.Position.X - 16,
            entity.Position.Y - 32
        );
    }
}