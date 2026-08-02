namespace Engine.Scene.Definitions;

public class DoorData
{
    public string TargetScene { get; }
    public string TargetSpawn { get; }

    public DoorData(
        string targetScene,
        string targetSpawn)
    {
        TargetScene = targetScene;
        TargetSpawn = targetSpawn;
    }
}