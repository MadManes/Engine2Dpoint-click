using Engine.Math;
using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Scene.Definitions;

public class EntityData
{
    public string Id { get; }
    public string Name { get; }    
    public Vector2 Position { get; }
    public SpriteData Sprite { get; }
    public EntityType Type { get; }  

    public EntityData(
        string id,
        string name,
        Vector2 position,
        SpriteData sprite,
        EntityType type)
    {
        Id = id;
        Name = name;
        Position = position;
        Sprite = sprite;
        Type = type;
    }
}