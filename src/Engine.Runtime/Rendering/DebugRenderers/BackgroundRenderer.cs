using Raylib_cs;

namespace Engine.Runtime.Rendering.DebugRenderers;

public class BackgroundRenderer
{
    public void Draw(Texture2D texture)
    {
        Raylib.DrawTexture(
            texture,
            0,
            0,
            Color.White
        );
    }
}