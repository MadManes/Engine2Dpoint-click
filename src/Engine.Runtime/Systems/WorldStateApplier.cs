using Engine.Game.State;
using Engine.Entities;
using Engine.Scene;

namespace Engine.Runtime.Systems;

public class WorldStateApplier
{
    private readonly EntityStateApplier _entityStateApplier;   

    public WorldStateApplier()
    {
        _entityStateApplier = new EntityStateApplier();        
    }

    public void Apply(
        GameState gameState,
        GameScene scene)
    {
        SceneState sceneState =
            gameState.WorldState.GetSceneState(
                scene.Name
            );

        foreach (var entity in scene.World.Entities.ToList())
        {
            EntityState entityState =
                sceneState.GetEntityState(
                    entity.EntityId
                );            
            
            if (!entityState.Exists)
            {
                scene.RemoveEntity(entity);
                continue;
            }

            _entityStateApplier.Apply(
                entity,
                entityState
            );
        }
    }
}
