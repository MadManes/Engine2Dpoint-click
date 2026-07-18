using Engine.Math;
using Engine.Scene.World;

namespace Engine.Runtime.Rendering;

public class RenderContext
{
    public World World { get; }

    public Vector2? MousePosition { get; }
    public Vector2? CorrectedPosition { get; }

    public RenderContext(
        World world,
        Vector2? mousePosition,
        Vector2? correctedPosition)
    {
        World = world;
        MousePosition = mousePosition;
        CorrectedPosition = correctedPosition;
    }
}