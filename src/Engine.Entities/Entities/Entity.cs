namespace Engine.Entities;

public class Entity
{
    public Guid Id { get; }
    public string Name { get; set; }
    public Transform Transform { get; }

    public Entity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Transform = new Transform();
    }
}