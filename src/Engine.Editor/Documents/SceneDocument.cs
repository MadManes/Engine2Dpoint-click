using Engine.Scene.Definitions;

namespace Engine.Editor.Documents;

public class SceneDocument
{
    public SceneData Scene { get; }
    public string FileName { get; }

    public bool IsDirty { get; private set; }


    public SceneDocument(
        SceneData scene,
        string fileName)
    {
        Scene = scene;
        FileName = fileName;
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