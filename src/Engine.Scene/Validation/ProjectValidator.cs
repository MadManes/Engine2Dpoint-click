using Engine.Scene.Definitions;

namespace Engine.Scene.Validation;

public class ProjectValidator : IProjectValidator
{
    public ValidationResult Validate(
        SceneProject project)
    {
        ValidationResult result = new();

        ValidateSceneIds(
            project,
            result
        );

        ValidateSceneReferences(
            project,
            result
        );

        ValidateStartupScene(
            project,
            result
        );

        return result;
    }


    private void ValidateSceneIds(
        SceneProject project,
        ValidationResult result)
    {
        HashSet<string> ids = new();


        foreach(SceneData scene in project.Scenes)
        {
            if(!ids.Add(scene.Id))
            {
                result.AddError(
                    "PR001",
                    $"Project contains duplicated scene id '{scene.Id}'."
                );
            }
        }
    }


    private void ValidateSceneReferences(
        SceneProject project,
        ValidationResult result)
    {
        foreach(SceneData scene in project.Scenes)
        {
            foreach(EntityData entity in scene.EntityCollection.Entities)
            {
                if(entity.Door == null)
                    continue;


                if(string.IsNullOrWhiteSpace(entity.Door.TargetScene))
                {
                    result.AddError(
                        "PR002",
                        $"Scene '{scene.Id}' entity '{entity.Id}' has a door without target scene."
                    );

                    continue;
                }


                SceneData? targetScene =
                    project.Scenes.FirstOrDefault(
                        x => x.Id == entity.Door.TargetScene
                    );


                if(targetScene == null)
                {
                    result.AddError(
                        "PR003",
                        $"Scene '{scene.Id}' entity '{entity.Id}' references missing scene '{entity.Door.TargetScene}'."
                    );

                    continue;
                }


                if(string.IsNullOrWhiteSpace(entity.Door.TargetSpawn))
                {
                    result.AddError(
                        "PR004",
                        $"Scene '{scene.Id}' entity '{entity.Id}' has a door without target spawn."
                    );

                    continue;
                }


                bool spawnExists =
                    targetScene.SpawnPoints.Any(
                        x => x.Id == entity.Door.TargetSpawn
                    );


                if(!spawnExists)
                {
                    result.AddError(
                        "PR005",
                        $"Scene '{targetScene.Id}' does not contain spawn '{entity.Door.TargetSpawn}'."
                    );
                }
            }
        }
    }

    private void ValidateStartupScene(
        SceneProject project,
        ValidationResult result)
    {
        if(string.IsNullOrWhiteSpace(
            project.StartupSceneId))
        {
            result.AddError(
                "PR006",
                "Project does not define a startup scene."
            );
    
            return;
        }
    
        bool exists =
            project.Scenes.Any(
                x => x.Id == project.StartupSceneId
            );
    
        if(!exists)
        {
            result.AddError(
                "PR007",
                $"Project startup scene '{project.StartupSceneId}' does not exist."
            );
        }
    }
}