using Engine.Editor.Loading;
using Engine.Editor.Documents;
using Engine.Entities;

namespace Engine.Editor.State;

public class EditorState
{
    public SceneDocument? CurrentDocument { get; private set; }

    public bool HasDocument =>  
        CurrentDocument != null;

    public Entity? SelectedEntity { get; private set; }

    public EditorAssets? Assets { get; set; }

    public void OpenDocument(
        SceneDocument document)
    {
        CurrentDocument = document;
        SelectedEntity = null;
        Assets = null;
    }

    public void SelectEntity(
        Entity? entity)
    {
        SelectedEntity = entity;
    }
}