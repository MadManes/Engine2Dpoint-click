using Engine.Editor.Project;
using Engine.Scene.Definitions;

namespace Engine.Editor.Creation;

public class EditorProjectFactory
{
    public EditorProject CreateEmpty()
    {
        ProjectData data =
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Untitled",
                Scenes = new List<string>(),
                StartupSceneId = ""
            };

        SceneProject source =
            new(
                Array.Empty<SceneData>(),
                ""
            );

        return new EditorProject(
            data,
            source,
            null
        );
    }
}