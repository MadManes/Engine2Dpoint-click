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
            if (!entity.Movement.Destination.HasValue)
                return;

            Vector2 currentPosition = entity.Transform.Position;
            Vector2 destination = entity.Movement.Destination.Value;

            float distance =
                currentPosition.Distance(destination);

            float step =
                entity.Movement.Speed * deltaTime;

            if (step >= distance)
            {
                entity.Transform.SetPosition(destination);
                entity.Movement.ClearDestination();
            }
            else
            {
                Vector2 direction =
                    (destination - currentPosition).Normalize();

                Vector2 displacement =
                    direction * step;

                entity.Transform.Translate(displacement);
            }
        }
    }
}