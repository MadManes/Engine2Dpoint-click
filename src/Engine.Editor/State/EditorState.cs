using Engine.Math;
using Engine.Editor.Loading;
using Engine.Editor.Documents;
using Engine.Scene.Definitions;

namespace Engine.Editor.State;

public class EditorState
{
    public SceneDocument? CurrentDocument { get; private set; }

    public bool HasDocument =>  
        CurrentDocument != null;

    public bool IsDragging {get; set; }

    public Vector2 DragOffset { get; set; }
    public EntityData? SelectedEntity { get; private set; }

    public EditorAssets? Assets { get; set; }

    public void OpenDocument(
        SceneDocument document)
    {
        CurrentDocument = document;
        SelectedEntity = null;
        Assets = null;
    }

    public void SelectEntity(
        EntityData? entity)
    {
        SelectedEntity = entity;
    }

    public void StartDragging(
        Vector2 offset)
    {
        IsDragging = true;
        DragOffset = offset;
    }
    
    
    public void StopDragging()
    {
        IsDragging = false;
    }
}