using Engine.Game.State;
using Engine.Scene;

namespace Engine.Game.Systems;

public class WorldStateApplier
{
    public void Apply(
        GameState gameState,
        GameScene scene)
    {
        SceneState sceneState =
            gameState.WorldState.GetSceneState(
                scene.Name
            );

        foreach (var entity in scene.World.Entities)
        {
            sceneState.GetEntityState(
                entity.EntityId
            );
        }
    }
}
