using Engine.Entities;
using Raylib_cs;

namespace Engine.Runtime.Rendering.DebugRenderers;

public class PlayerDebugRenderer
{
    private readonly PrimitiveRenderer _primitiveRenderer;


    public PlayerDebugRenderer(
        PrimitiveRenderer primitiveRenderer)
    {
        _primitiveRenderer = primitiveRenderer;
    }


    public void Draw(Entity player)
    {
        _primitiveRenderer.DrawCircle(
            player.Transform.Position,
            10,
            Color.SkyBlue
        );
    }
}