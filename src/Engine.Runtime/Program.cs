using Engine.Core;
using Engine.Runtime;
using Engine.Entities;
using Engine.Math;
using Engine.Scene.World;


Polygon polygon = new();
polygon.AddVertex(new Vector2(0, 0));
polygon.AddVertex(new Vector2(800, 0));
polygon.AddVertex(new Vector2(800, 600));
polygon.AddVertex(new Vector2(0, 600));

WalkableArea area = new(
    NavigationType.Polygon, 
    polygon);

World world = new(area);

// Print the number of vertices in the polygon
Console.WriteLine("Number of vertices in the polygon: " + area.Polygon.Vertices.Count);

Polygon polygon2 = new(
    new Vector2(0,0),
    new Vector2(800,0),
    new Vector2(800,600),
    new Vector2(0,600)
);

Console.WriteLine(
    polygon2.Contains(
        new Vector2(200,300)));

Console.WriteLine(
    polygon2.Contains(
        new Vector2(900,300)));

LineSegment segment = new(
    new Vector2(0,0),
    new Vector2(100,0)
);

// Proof of concept for LineSegment and ClosestPoint method
Vector2 result = segment.ClosestPoint(
    new Vector2(50,50)
);
Console.WriteLine(
    $"Closest: {result.X}, {result.Y}");

Vector2 result2 = segment.ClosestPoint(
    new Vector2(200,20)
);
Console.WriteLine(
    $"Closest: {result2.X}, {result2.Y}");

Polygon room1 = new(
    new Vector2(0,0),
    new Vector2(800,0),
    new Vector2(800,600),
    new Vector2(0,600)
);


Vector2 click = new(900,300);


Vector2 corrected = room1.GetClosestPoint(click);


Console.WriteLine(
    $"Corrected: {corrected.X}, {corrected.Y}");

// Create a new scene called "Puerto"
//Scene room = new("Puerto", world); 

// Create a new player entity
//Entity player = new("Guybrush");

// Add the player entity to the scene
//room.AddEntity(player);

// Print the scene name and the number of entities in the scene
//Console.WriteLine(room.Name);
//Console.WriteLine(room.Entities.Count);

// Create a new game instance
Game game = new();

// Set the player's position to (150, 80)
//player.Transform.SetPosition(new Vector2(150, 80));

// Proof of concept for Vector2 and Transform classes
Vector2 a = new(3,4);
Console.WriteLine(a.Length());
Vector2 direction = a.Normalize();
Console.WriteLine(direction);

// Show the player's position in the console
//Console.WriteLine(player.Transform.Position);

GameEngine engine = new(game);
engine.Run();