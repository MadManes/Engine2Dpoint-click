using Engine.Core;
using Engine.Runtime;
using Engine.Entities;
using Engine.Math;

Game game = new();

Entity player = new("Player");

player.Transform.Position = new Vector2(150, 80);

Console.WriteLine(player.Transform.Position);

GameEngine engine = new(game);
engine.Run();