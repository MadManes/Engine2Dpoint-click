using Engine.Math;
using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Scene.Definitions;

public class EntityData
{
    public string Name { get; }
    public Vector2 Position { get; }
    public SpriteData Sprite { get; }
    public EntityType Type { get; }  

    public EntityData(
        string name,
        Vector2 position,
        SpriteData sprite,
        EntityType type)
    {
        Name = name;
        Position = position;
        Sprite = sprite;
        Type = type;
    }
}