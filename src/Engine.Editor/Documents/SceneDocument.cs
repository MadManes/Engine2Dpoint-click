using Engine.Scene.Definitions;

namespace Engine.Editor.Documents;

public class SceneDocument
{
    public SceneData Scene { get; }

    public bool IsDirty { get; private set; }


    public SceneDocument(
        SceneData scene)
    {
        Scene = scene;
    }


    public void MarkDirty()
    {
        IsDirty = true;
    }


    public void SaveCompleted()
    {
        IsDirty = false;
    }
}