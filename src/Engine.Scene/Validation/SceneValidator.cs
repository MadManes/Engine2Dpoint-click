using Engine.Scene.Definitions;

namespace Engine.Scene.Validation;

public class SceneValidator : ISceneValidator
{
    public ValidationResult Validate(SceneData scene)
    {
        ValidationResult result = new();
        
        ValidateIdentity(scene, result);
        ValidateNavigation(scene, result);
        ValidateSpawnPoints(scene, result);
        ValidateEntities(scene, result);        

        return result;
    }


    private void ValidateIdentity(SceneData scene, ValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(scene.Id))
        {
            result.AddError(
                "SC001",
                "Scene Id cannot be empty."
            );
        }


        if (string.IsNullOrWhiteSpace(scene.Name))
        {
            result.AddError(
                "SC002",
                $"Scene '{scene.Id}' has no name."
            );
        }

        if (string.IsNullOrWhiteSpace(scene.Background.AssetName))
        {
            result.AddWarning(
                "SC003",
                "Scene requires a background."
            );
        }
    }


    private void ValidateNavigation(SceneData scene, ValidationResult result)
    {
        if (scene.Navigation.Vertices.Count < 3)
        {
            result.AddWarning(
                "SC004",
                $"Scene '{scene.Id}' navigation polygon requires at least 3 vertices."
            );            
        }
    }


    private void ValidateSpawnPoints(SceneData scene, ValidationResult result)
    {
        bool hasDefault =
            scene.SpawnPoints.Any(
                x => x.Id == "Default"
            );

        if (!hasDefault)
        {
            result.AddWarning(
                "SC005",
                $"Scene '{scene.Id}' does not contain a 'Default' spawn point."
            );
        }
        HashSet<string> ids = new();

        foreach (SpawnPointData spawn in scene.SpawnPoints)
        {
            if (string.IsNullOrWhiteSpace(spawn.Id))
            {
                result.AddError(
                    "SC006",
                    $"Scene '{scene.Id}' contains a spawn point without id."
                );
            }


            if (!ids.Add(spawn.Id))
            {
                result.AddError(
                    "SC007",
                    $"Scene '{scene.Id}' contains duplicated spawn point '{spawn.Id}'."
                );
            }
        }
    }


    private void ValidateEntities(SceneData scene, ValidationResult result)
    {
        HashSet<string> ids = new();

        foreach (EntityData entity in scene.EntityCollection.Entities)
        {
            if (string.IsNullOrWhiteSpace(entity.Id))
            {
                result.AddError(
                    "SC008",
                    $"Scene '{scene.Id}' contains an entity without id."
                );
            }


            if (!ids.Add(entity.Id))
            {
                result.AddError(
                    "SC009",
                    $"Scene '{scene.Id}' contains duplicated entity '{entity.Id}'."
                );
            }
        }
    }

}