using Engine.Core;
using Engine.Runtime;
using Engine.Entities;
using Engine.Math;
using Engine.Scene;

// Create a new scene called "Puerto"
Scene room = new("Puerto");

// Create a new player entity
Entity player = new("Guybrush");

// Add the player entity to the scene
room.AddEntity(player);

// Print the scene name and the number of entities in the scene
Console.WriteLine(room.Name);
Console.WriteLine(room.Entities.Count);

// Create a new game instance
Game game = new();

// Set the player's position to (150, 80)
player.Transform.SetPosition(new Vector2(150, 80));

// Proof of concept for Vector2 and Transform classes
Vector2 a = new(3,4);
Console.WriteLine(a.Length());
Vector2 direction = a.Normalize();
Console.WriteLine(direction);

// Show the player's position in the console
Console.WriteLine(player.Transform.Position);

GameEngine engine = new(game);
engine.Run();