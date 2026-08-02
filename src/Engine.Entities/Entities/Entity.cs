using Engine.Entities.Components;

namespace Engine.Entities;

public class Entity
{
    public Guid Id { get; }
    public string EntityId { get; }
    public string Name { get; set; }
    public bool Visible { get; set; } = true;
    public Transform Transform { get; }
    public Movement Movement { get; }
    public EntityType Type { get; }
    public SpriteComponent Sprite { get; }
    public InteractionComponent? Interaction { get; private set; }
    public ComponentCollection Components { get; }
    
    public Entity(
        string entityId,
        string name, 
        EntityType type,
        SpriteComponent sprite        
        )
    {
        Id = Guid.NewGuid();

        EntityId = entityId;
        Name = name;
        Type = type;
        Sprite = sprite;
        Sprite.Entity = this;

        Transform = new Transform();
        Transform.Entity = this;

        Movement = new Movement();
        Movement.Entity = this;
        Components = new ComponentCollection();
    }

    public void  EnableInteraction()
    {
        Interaction = new InteractionComponent();
        Interaction.Entity = this;
    }
}