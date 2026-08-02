namespace Engine.Entities.Components;

public class DoorComponent : Component
{
    public string TargetScene { get; }
    public string TargetSpawn { get; }

    public DoorComponent(
        string targetScene,
        string targetSpawn)
    {
        TargetScene = targetScene;
        TargetSpawn = targetSpawn;
    }
}