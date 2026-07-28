using Engine.Math;
using Engine.Scene;

namespace Engine.Runtime.Rendering;

public class RenderContext
{
    public GameScene Scene { get; }
    public string SceneName { get; }    
    public Vector2? MousePosition { get; }
    public Vector2? CorrectedPosition { get; }
    

    public RenderContext(
        GameScene scene,
        string sceneName,        
        Vector2? mousePosition,
        Vector2? correctedPosition
    )        
    {
        Scene = scene;
        SceneName = sceneName;        
        MousePosition = mousePosition;
        CorrectedPosition = correctedPosition;        
    }
}