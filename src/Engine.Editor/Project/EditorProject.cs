using Engine.Editor.Documents;
using Engine.Scene.Definitions;

namespace Engine.Editor.Project;

public class EditorProject
{
    private readonly Dictionary<string, SceneDocument> _documents;

    public SceneProject Source { get; }

    public IReadOnlyCollection<SceneDocument> Documents =>
        _documents.Values;
    public IReadOnlyCollection<string> SceneIds =>
        _documents.Keys;

    public SceneDocument? CurrentDocument { get; private set; }
    
    public bool IsDirty =>
        _documents.Values.Any(
            document => document.IsDirty
        );

    public EditorProject(
        SceneProject source)
    {
        Source = source;

        _documents = new Dictionary<string, SceneDocument>();
    }


    public void AddDocument(
        SceneDocument document)
    {
        _documents.Add(
            document.Scene.Id,
            document
        );
    }


    public void OpenDocument(
        string sceneId)
    {
        if(!_documents.TryGetValue(
            sceneId,
            out SceneDocument? document))
        {
            throw new InvalidOperationException(
                $"Scene '{sceneId}' is not open in the editor project."
            );
        }

        CurrentDocument = document;
    }

    public SceneDocument GetDocument(
    string sceneId)
    {
        if(!_documents.TryGetValue(
            sceneId,
            out SceneDocument? document))
        {
            throw new InvalidOperationException(
                $"Scene '{sceneId}' is not open in the editor project."
            );
        }

        return document;
    }

}