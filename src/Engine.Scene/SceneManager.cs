using Engine.Entities;

namespace Engine.Scene;

public class SceneManager
{
    public GameScene? CurrentScene { get; private set; }

    public void LoadScene(
        GameScene scene,
        IEnumerable<Entity>? persistentEntities = null)
    {
        CurrentScene = scene;

        if (persistentEntities == null)
        {
            return;
        }

        foreach (Entity entity in persistentEntities)
        {
            CurrentScene.AddPersistentEntity(entity);
        }
    }
}