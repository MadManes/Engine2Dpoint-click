using Engine.Entities;
using Engine.Game.Entities;

namespace Engine.Game.State;

public class GameState
{
    public PersistentEntityCollection PersistentEntities { get; }
    public Entity Player { get; }
    public WorldState WorldState { get; }

    public GameState(Entity player)
    {
        PersistentEntities = new PersistentEntityCollection();
        WorldState = new WorldState();
        Player = player;
        PersistentEntities.Add(player);
    }
}