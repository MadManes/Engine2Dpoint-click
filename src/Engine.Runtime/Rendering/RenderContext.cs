using Engine.Math;
using Engine.Entities;
using Engine.Scene;

namespace Engine.Runtime.Rendering;

public class RenderContext
{
    public GameScene Scene { get; }
    public string SceneName { get; }    
    public Vector2? MousePosition { get; }
    public Vector2? CorrectedPosition { get; }
    public Entity Player { get; }

    public RenderContext(
        GameScene scene,
        string sceneName,
        Entity player,       
        Vector2? mousePosition,
        Vector2? correctedPosition
    )        
    {
        Scene = scene;
        SceneName = sceneName;
        Player = player;
        MousePosition = mousePosition;
        CorrectedPosition = correctedPosition;        
    }
}