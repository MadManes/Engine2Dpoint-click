namespace Engine.Core;

public class GameEngine
{
    private readonly IGameLoop _gameLoop;

    private bool _running = true;    

    public GameEngine(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
    }

    public void Run()
    {
        _gameLoop.Initialize();

        while (_running) // Main game loop
        {
            float deltaTime = Time.DeltaTime();
            _gameLoop.Update(deltaTime);
            _gameLoop.Render();
        }

        _gameLoop.Shutdown(); 
    }

    public void Stop()
    {
        _running = false;
    }
}