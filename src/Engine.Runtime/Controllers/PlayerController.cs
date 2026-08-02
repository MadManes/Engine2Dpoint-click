using Engine.Core;
using Engine.Entities;
using Engine.Math;

namespace Engine.Runtime.Controllers;

public class PlayerController
{
    private readonly Entity _player;

    public PlayerController(Entity player)
    {
        _player = player;
    }


    public void MoveTo(
        NavigationPath path)
    {
        _player.Movement.SetPath(path);
    }
}