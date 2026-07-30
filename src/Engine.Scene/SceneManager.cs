using Engine.Entities;
using Engine.Entities.Components;
using Engine.Game.Entities;

namespace Engine.Scene;

public class SceneManager
{
    public GameScene? CurrentScene { get; private set; }

    private readonly PersistentEntityCollection _persistentEntities = new();

    public void RegisterPersistentEntity(Entity entity)
    {                
        _persistentEntities.Add(entity);        
    }

    public void LoadScene(GameScene scene)
    {
        CurrentScene = scene;      

        foreach (Entity entity in _persistentEntities.Entities)
        {
            if (entity.Type == EntityType.Player)
            {
                entity.Transform.SetPosition(
                    scene.Data.PlayerSpawn
                );

                entity.Movement.ClearPath();
            }
            CurrentScene.AddPersistentEntity(entity);
        }
    }
}