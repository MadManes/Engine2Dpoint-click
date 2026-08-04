using Engine.Editor.Documents;
using Engine.Entities;

namespace Engine.Editor.State;

public class EditorState
{
    public SceneDocument? CurrentDocument { get; private set; }

    public Entity? SelectedEntity { get; private set; }


    public void OpenDocument(
        SceneDocument document)
    {
        CurrentDocument = document;
        SelectedEntity = null;
    }


    public void SelectEntity(
        Entity? entity)
    {
        SelectedEntity = entity;
    }
}