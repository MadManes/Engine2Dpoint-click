using Engine.Math;
using Engine.Entities;
using Engine.Scene.World;

namespace Engine.Runtime.Rendering;

public class RenderContext
{
    public World World { get; }
    public Entity Player { get; }

    public Vector2? MousePosition { get; }
    public Vector2? CorrectedPosition { get; }

    public RenderContext(
        World world,
        Entity player,
        Vector2? mousePosition,
        Vector2? correctedPosition)
    {
        World = world;
        Player = player;
        MousePosition = mousePosition;
        CorrectedPosition = correctedPosition;
    }
}