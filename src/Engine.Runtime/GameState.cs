using Engine.Entities;

namespace Engine.Runtime;

public class GameState
{
    public Entity Player { get; }

    public GameState(Entity player)
    {
        Player = player;
    }
}