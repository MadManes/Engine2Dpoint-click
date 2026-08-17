using Engine.Math;
using Engine.Editor.Documents;
using Engine.Editor.Creation;
using Engine.Scene.Definitions;
using Engine.Editor.UI;

namespace Engine.Editor.Project;

public class EditorProject
{
    private readonly Dictionary<string, SceneDocument> _documents;

    private readonly EditorSceneFactory _sceneFactory;

    public ProjectData Data { get; }
    public SceneProject Source { get; }

    public string? FilePath { get; private set; }

    public string ProjectDirectory =>
        FilePath == null
            ? throw new InvalidOperationException(
                "Project does not have a file path."
            )
            : Path.GetDirectoryName(FilePath)
                ?? throw new InvalidOperationException(
                    "Project file path is invalid."
                );
    
    public string SceneDirectory =>
        Path.Combine(
            ProjectDirectory,
            "scenes"
        );

    //public string? FilePath { get; private set; }

    public bool IsProjectDirty { get; private set; }

    public IReadOnlyCollection<SceneDocument> Documents =>
        _documents.Values;
    public IReadOnlyCollection<string> SceneIds =>
        _documents.Keys;

    public SceneDocument? CurrentDocument { get; private set; }

    public IReadOnlyCollection<EditorSceneOption> SceneOptions
    {
        get
        {
            return _documents.Values
                .Select(
                    document => new EditorSceneOption(
                        document.Scene.Id,
                        document.Scene.Name
                    )
                )
                .ToList();            
        }
    }
    
    public bool IsDirty =>
        IsProjectDirty ||
        _documents.Values.Any(
            document => document.IsDirty
        );

    public EditorProject(
        ProjectData data,
        SceneProject source,
        string? filePath = null)
    {
        Data = data;
        Source = source;
        FilePath = filePath;

        _documents = new Dictionary<string, SceneDocument>();
        _sceneFactory = new EditorSceneFactory();
    }


    public void SetFilePath(
        string filePath)
    {
        if(string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException(
                "Project file path cannot be empty.",
                nameof(filePath)
            );

        FilePath = filePath;
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

    public SceneDocument CreateScene(
        string id,
        string name)
    {
        if(string.IsNullOrWhiteSpace(id))
            throw new ArgumentException(
                "Scene id cannot be empty.",
                nameof(id)
            );

        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Scene name cannot be empty.",
                nameof(name)
            );

        if(_documents.ContainsKey(id))
            throw new InvalidOperationException(
                $"Scene '{id}' already exists."
            );

        if(Data.Scenes.Contains(id))
            throw new InvalidOperationException(
                $"Project already contains scene '{id}'."
            );


        SceneData scene =
            _sceneFactory.Create(
                id,
                name
            );


        SceneDocument document =
            new(
                scene,
                $"{id}.json",
                true
            );


        AddDocument(
            document
        );

        Data.Scenes.Add(
            id
        );

        MarkProjectDirty();

        return document;
    }

    public void RenameScene(
        string sceneId,
        string name)
    {
        if(!_documents.TryGetValue(
            sceneId,
            out SceneDocument? document))
        {
            throw new InvalidOperationException(
                $"Scene '{sceneId}' does not exist in the editor project."
            );
        }

        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Scene name cannot be empty.",
                nameof(name)
            );
        }

        document.SetName(
            name
        );
    }

    public void DeleteScene(
        string sceneId)
    {
        if(!_documents.TryGetValue(
            sceneId,
            out SceneDocument? document))
        {
            throw new InvalidOperationException(
                $"Scene '{sceneId}' does not exist in the editor project."
            );
        }
    
    
        if(Data.Scenes.Count <= 1)
        {
            throw new InvalidOperationException(
                "A project must contain at least one scene."
            );
        }
    
    
        if(Data.StartupSceneId == sceneId)
        {
            throw new InvalidOperationException(
                "Cannot delete the startup scene."
            );
        }
    
    
        _documents.Remove(
            sceneId
        );
    
        Data.Scenes.Remove(
            sceneId
        );
    
    
        if(CurrentDocument == document)
        {
            CurrentDocument = null;
        }
    
    
        MarkProjectDirty();
    }


    public void MarkProjectDirty()
    {
        IsProjectDirty = true;
    }

    public void SaveProjectCompleted()
    {
        IsProjectDirty = false;
    }

}