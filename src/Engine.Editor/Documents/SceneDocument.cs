using Engine.Scene.Definitions;

namespace Engine.Editor.Documents;

public class SceneDocument
{
    public SceneData Scene { get; }

    public string FileName { get; }

    public bool IsDirty { get; private set; }


    public SceneDocument(
        SceneData scene,
        string fileName,
        bool isDirty)
    {
        Scene = scene;
        FileName = fileName;
        IsDirty = isDirty;
    }    

    public void SetName(
        string name)
    {
        Scene.Name = name;
        MarkDirty();
    }
    
    
    public void SetBackground(
        string assetName)
    {
        Scene.Background.AssetName = assetName;
        MarkDirty();
    }
    
    
    public void AddSpawnPoint(
        SpawnPointData spawnPoint)
    {
        Scene.SpawnPoints.Add(
            spawnPoint
        );
    
        MarkDirty();
    }
    
    
    public void RemoveSpawnPoint(
        string id)
    {
        SpawnPointData? spawn =
            Scene.SpawnPoints.FirstOrDefault(
                x => x.Id == id
            );
    
        if(spawn == null)
            return;
    
        Scene.SpawnPoints.Remove(
            spawn
        );
    
        MarkDirty();
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