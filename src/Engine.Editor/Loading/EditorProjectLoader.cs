using Engine.Scene.Definitions;
using Engine.Scene.Loading.Project;
using Engine.Scene.Loading.Sources;
using Engine.Scene.Serialization;
using Engine.Scene.Validation;

namespace Engine.Editor.Loading;

public class EditorProjectLoader
{
    private readonly string _sceneFolder;


    public EditorProjectLoader(
        string sceneFolder)
    {
        _sceneFolder = sceneFolder;
    }


    public SceneProject Load(
        ProjectData projectData)
    {
        SceneSerializer serializer = new();

        SceneValidator validator = new();

        JsonSceneSource source =
            new(
                _sceneFolder,
                serializer,
                validator
            );


        ProjectValidator projectValidator =
            new();


        SceneProjectLoader loader =
            new(
                source,
                projectValidator
            );


        return loader.Load(
            projectData.Scenes,
            projectData.StartupSceneId
        );
    }
}