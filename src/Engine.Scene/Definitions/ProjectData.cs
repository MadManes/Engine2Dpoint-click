namespace Engine.Scene.Definitions;

public class ProjectData
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";    

    public List<string> Scenes { get; set; } = new();

    public string StartupSceneId { get; set; } = "";
}