namespace Engine.Runtime;

using Engine.Core;

public class Game : IGameLoop
{
    private int frames = 0;

    public void Initialize()
    {
        // Initialization logic for the game
        Console.WriteLine("Game initialized.");
    }

    public void Update(float deltaTime)
    {
        // Update game state based on deltaTime
        Console.WriteLine($"Game updated with deltaTime: {deltaTime}");
        frames++;
        if(frames >= 5) // Stop the game after 5 frames for demonstration
        {
            Console.WriteLine("Stopping the game after 5 frames.");
            Environment.Exit(0); // Exit the application
        }
    }

    public void Render()
    {
        // Render the game visuals
        Console.WriteLine("Game rendered.");
    }

    public void Shutdown()
    {
        // Cleanup resources and shutdown logic
        Console.WriteLine("Game shutdown.");
    }
}