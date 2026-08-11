using Engine.Editor.Loading;
using Engine.Editor.Documents;
using Engine.Editor.Project;
using Engine.Scene.Definitions;

namespace Engine.Editor.State;

public class EditorState
{
    public EditorProject? Project { get; private set; }

    public SceneDocument? CurrentDocument =>
        Project?.CurrentDocument;

    public bool HasDocument =>
        CurrentDocument != null;

    public EntityData? SelectedEntity { get; private set; }

    public EditorAssets? Assets { get;  private set; }


    public void OpenProject(
        EditorProject project)
    {
        Project = project;
        SelectedEntity = null;
        Assets = null;
    }

    public void OpenScene(
        string sceneId)
    {
        if(Project == null)
            throw new InvalidOperationException(
                "No project is open."
            );

        Project.OpenDocument(
            sceneId
        );

        SelectedEntity = null;
        Assets = null;
    }

    public void SelectEntity(
        EntityData? entity)
    {
        SelectedEntity = entity;
    }

    public void SetAssets(
        EditorAssets assets)
    {
        Assets = assets;
    }

    public void MarkCurrentDocumentDirty()
    {
        if(CurrentDocument == null)
            return;
    
        CurrentDocument.MarkDirty();
    }
}