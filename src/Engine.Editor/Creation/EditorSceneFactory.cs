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

            Navigation = new NavigationData
            {
                Vertices = new List<Engine.Math.Vector2>
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
                    Position = new Engine.Math.Vector2(
                        640,
                        320
                    )
                }
            }
        };
    }
}