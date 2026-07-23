using Engine.Core;
using Engine.Math;
using Engine.Entities;
using Engine.Scene;

namespace Engine.Runtime.Rendering;

public class RenderContext
{
    public GameScene Scene { get; }
    public Entity Player { get; }
    public Vector2? MousePosition { get; }
    public Vector2? CorrectedPosition { get; }    

    public RenderContext(
        GameScene scene,
        Entity player,
        Vector2? mousePosition,
        Vector2? correctedPosition
    )        
    {
        Scene = scene;
        Player = player;
        MousePosition = mousePosition;
        CorrectedPosition = correctedPosition;        
    }
}