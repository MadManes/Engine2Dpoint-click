using Engine.Editor.Documents;
using Engine.Editor.Loading;
using Engine.Scene.Definitions;

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
        EditorAssets assets,
        EntityData? selectedEntity
        )
    {
        if(document == null)
            return;


        _sceneViewRenderer.Render(
            document,
            assets  ,
            selectedEntity          
        );
    }
}