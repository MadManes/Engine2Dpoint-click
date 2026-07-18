using Engine.Scene.World;

namespace Engine.Runtime.Rendering;

public interface IRenderer
{
    void Initialize();

    void Render(RenderContext context);

    void Shutdown();
}