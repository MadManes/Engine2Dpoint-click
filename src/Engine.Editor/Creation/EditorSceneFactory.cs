using Engine.Math;
using Engine.Scene.Definitions;

namespace Engine.Editor.Creation;

public class EditorSceneFactory
{
    public SceneData Create(
        string id,
        string name)
    {
        return new SceneData
        {
            Id = id,
            Name = name,

            Background = new BackgroundData
            {
                AssetName = ""
            },

            Navigation = new NavigationData
            {
                Vertices = new List<Vector2>
                {
                    new(0, 0),
                    new(1280, 0),
                    new(1280, 640),
                    new(0, 640)
                }
            },

            SpawnPoints = new List<SpawnPointData>
            {
                new()
                {
                    Id = "Default",
                    Position = new Vector2(640, 320)
                }
            }
        };
    }
}