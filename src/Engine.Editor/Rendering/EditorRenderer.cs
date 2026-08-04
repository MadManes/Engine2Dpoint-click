using Engine.Editor.Documents;
using Engine.Editor.Loading;
using Raylib_cs;

namespace Engine.Editor.Rendering;

public class EditorRenderer
{
    private readonly SceneViewRenderer _sceneViewRenderer;

    public EditorRenderer()
    {
        _sceneViewRenderer =
            new SceneViewRenderer();
    }

    public void Render(
        SceneDocument? document,
        EditorAssets assets)
    {
        if(document == null)
            return;


        _sceneViewRenderer.Render(
            document,
            assets
        );
    }
}