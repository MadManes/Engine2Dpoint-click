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
                    $"Duplicated scene id '{scene.Id}'."
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


                SceneData? targetScene =
                    project.Scenes.FirstOrDefault(
                        x => x.Id == entity.Door.TargetScene
                    );


                if(targetScene == null)
                {
                    result.AddError(
                        "PR002",
                        $"Scene '{scene.Id}' entity '{entity.Id}' references missing scene '{entity.Door.TargetScene}'."
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
                        "PR003",
                        $"Scene '{targetScene.Id}' does not contain spawn '{entity.Door.TargetSpawn}'."
                    );
                }
            }
        }
    }
}