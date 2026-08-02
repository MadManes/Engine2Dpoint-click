using Engine.Math;
using Engine.Entities;
using Engine.Entities.Components;

namespace Engine.Scene.Definitions;

public class EntityData
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public Vector2 Position { get; set; }
    public SpriteData Sprite { get; set; } = new();
    public EntityType Type { get; set; } 
    public bool IsInteractable { get; set; }
    public DoorData? Door { get; set; }

}