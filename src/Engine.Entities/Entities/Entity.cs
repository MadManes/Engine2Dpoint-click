using Engine.Entities.Components;
namespace Engine.Entities;

public class Entity
{
    public Guid Id { get; }
    public string Name { get; set; }
    public Transform Transform { get; }
    public Movement Movement { get; }

    public Entity(string name)
    {
        Id = Guid.NewGuid();

        Name = name;

        Transform = new Transform();

        Transform.Entity = this;

        Movement = new Movement();

        Movement.Entity = this;
    }
}