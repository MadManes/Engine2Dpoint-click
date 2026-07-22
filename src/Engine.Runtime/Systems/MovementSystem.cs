using Engine.Entities;
using Engine.Scene.World;
using Engine.Math;

namespace Engine.Runtime.Systems;

public class MovementSystem
{
    public void Update(World world, float deltaTime)
    {
        foreach (var entity in world.Entities)
        {
            if (entity.Movement.Path == null ||
                entity.Movement.Path.IsEmpty)
            {
                continue;
            }


            Vector2 currentPosition =
                entity.Transform.Position;


            Vector2 destination =
                entity.Movement.CurrentTarget;


            float distance =
                currentPosition.Distance(destination);


            float step =
                entity.Movement.Speed * deltaTime;


            if (step >= distance)
            {
                entity.Transform.SetPosition(destination);


                bool hasNextPoint =
                    entity.Movement.AdvancePath();


                if (!hasNextPoint)
                {
                    entity.Movement.ClearPath();
                }
            }
            else
            {
                Vector2 direction =
                    (destination - currentPosition)
                    .Normalize();


                entity.Transform.Translate(
                    direction * step
                );
            }
        }
    }
}